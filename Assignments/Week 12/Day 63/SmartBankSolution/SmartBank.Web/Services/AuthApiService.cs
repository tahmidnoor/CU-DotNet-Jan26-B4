
using System.Text;
using System.Text.Json;
using SmartBank.Web.Models;

namespace SmartBank.Web.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _client;

        public AuthApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Login(string email, string password)
        {
            var payload = new { email, password };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            dynamic obj = JsonSerializer.Deserialize<dynamic>(json);
            string token = obj.GetProperty("token").GetString();

            return token;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("api/auth/register", content);

            return response.IsSuccessStatusCode;
        }

        private class AuthResponse
        {
            public string access_token { get; set; }
        }
    }
}