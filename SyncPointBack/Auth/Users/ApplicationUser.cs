using Microsoft.AspNetCore.Identity;

namespace SyncPointBack.Auth.Users
{
    public enum Role
    {
        Admin,
        User
    }

    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }

        public bool isActive { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }
    }
}