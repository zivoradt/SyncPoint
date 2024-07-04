using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SyncPointBack.Auth;
using SyncPointBack.Auth.Requests;
using SyncPointBack.Auth.Users;
using SyncPointBack.Helper.ErrorHandler.CustomException;
using SyncPointBack.Persistance;
using SyncPointBack.Persistance.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyncPointBack.Services.AuthService
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly AppSettings _appSettings;

        private const int ExpirationDays = 60;

        private readonly ILogger<UserService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IOptions<AppSettings> appSettings, UnitOfWork unitOfWork, ILogger<UserService> logger, UserManager<ApplicationUser> userManager)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task RegisterUser(RegistrationRequest request)
        {
            var errors = new List<string>();

            if (!IsValidRegistrationRequest(request))
            {
                errors.Add("Invalid registration request.");
            }

            var result = await _userManager.CreateAsync(
                new ApplicationUser { UserName = request.Username, Email = request.Email, Role = Role.User, isActive = true },
                request.Password!);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
            }

            if (errors.Count > 0)
            {
                throw new RegistrationException(errors);
            }

            _logger.LogInformation("User registered successfully: {Username}", request.Username);
        }

        public async Task<ApplicationUser> GetApplicationUserByEmail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(r => r.Email == email);

            return user;
        }

        public async Task<ApplicationUser> GetUserByEmailAsyncFromDB(string email)
        {
            var user = await _unitOfWork.AuthRepository.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<bool> CheckIfPasswordIsValid(ApplicationUser user, string password)
        {
            var isValid = await _userManager.CheckPasswordAsync(user, password);

            if (isValid)
            {
                return true;
            }
            return false;
        }

        private bool IsValidRegistrationRequest(RegistrationRequest request)
        {
            // Perform additional validation logic here (e.g., email format, password complexity)
            return request != null && !string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password);
        }

        public string CreateToken(ApplicationUser user)
        {
            var expiration = DateTime.Now.AddDays(60);
            var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);

            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT Token created");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var validIssuer = configuration.GetSection("JwtTokenSettings")["ValidIssuer"];
            var validAudience = configuration.GetSection("JwtTokenSettings")["ValidAudience"];

            return new JwtSecurityToken(
                issuer: validIssuer,
                audience: validAudience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }

        private List<Claim> CreateClaims(ApplicationUser user)
        {
            var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["JwtRegisteredClaimNamesSub"];

            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                return claims;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["SymmetricSecurityKey"];

            return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
            SecurityAlgorithms.HmacSha256
            );
        }
    }
}