namespace SyncPointBack.Auth.Requests
{
    public class AuthResponse
    {
        public string Email { get; set; }

        public string Username { get; set; }
        public bool isActive { get; set; }
        public string Token { get; set; }
    }
}