using Microsoft.AspNetCore.Identity;
using SyncPointBack.Auth.Requests;
using SyncPointBack.Auth.Users;

namespace SyncPointBack.Services.AuthService
{
    public interface IUserService
    {
        Task RegisterUser(RegistrationRequest request);

        Task<ApplicationUser> GetApplicationUserByEmail(string email);

        Task<ApplicationUser> GetUserByEmailAsyncFromDB(string email);

        Task<bool> CheckIfPasswordIsValid(ApplicationUser user, string password);

        string CreateToken(ApplicationUser user);
    }
}