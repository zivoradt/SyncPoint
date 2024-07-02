using SyncPointBack.Auth.Users;
using System.ComponentModel.DataAnnotations;

namespace SyncPointBack.Auth.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}