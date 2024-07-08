namespace SyncPointBack.Auth.Requests
{
    public class RefreshTokenRequest
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}