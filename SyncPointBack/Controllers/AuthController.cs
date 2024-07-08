using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPointBack.Auth.Requests;
using SyncPointBack.Services.AuthService;

namespace SyncPointBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private readonly IUserService _userService;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var IsEmailExist = await _userService.GetApplicationUserByEmail(request.Email);

            if (IsEmailExist != null)
            {
                return BadRequest("User with this email is already exist");
            }

            await _userService.RegisterUser(request);

            return Ok("User is registered");
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var loginResult = await _userService.RefreshToken(request);

            if (loginResult.isLoggedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetApplicationUserByEmail(request.Email!);

            if (user == null)
            {
                return BadRequest("Email dont exist");
            }

            var isPasswordValid = await _userService.CheckIfPasswordIsValid(user, request.Password!);

            if (!isPasswordValid)
            {
                return BadRequest("Wrong password");
            }

            var userDb = await _userService.GetUserByEmailAsyncFromDB(request.Email);

            if (userDb is null)
            {
                return Unauthorized();
            }

            var token = _userService.CreateToken(user);

            var refreshToken = _userService.GenerateRefreshToken();

            userDb.RefreshToken = refreshToken;
            userDb.RefreshTokenExpiry = DateTime.Now.AddMinutes(20);

            if (await _userService.UpdateRefreshTokenDetails(userDb))
            {
                _logger.LogInformation("Refresh token updated");
            }
            else
            {
                throw new InvalidOperationException("Refresh Token operation is failed.");
            }

            return Ok(new AuthResponse
            {
                JwtToken = token,
                Email = request.Email,
                Username = userDb.UserName,
                isActive = userDb.isActive,
                RefreshToken = refreshToken
            });
        }
    }
}