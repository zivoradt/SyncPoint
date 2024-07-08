namespace SyncPointBack.Auth.Requests
{
    public class AuthResponse
    {
        public string Email { get; set; }

        public string Username { get; set; }
        public bool isActive { get; set; }

        public bool isLoggedIn { get; set; } = false;
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}