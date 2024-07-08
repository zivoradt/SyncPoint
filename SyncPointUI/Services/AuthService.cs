namespace SyncPointUI.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Authenticate(AuthRequest authRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Excel/login", authRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
    }

    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool isActive { get; set; }
    }
}