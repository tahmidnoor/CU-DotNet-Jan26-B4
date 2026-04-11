using System.Net.Http.Headers;
using System.Text.Json;
using SmartBank.Web.Models;

namespace SmartBank.Web.Services
{
    public class AccountApiService
    {
        private readonly HttpClient _client;

        public AccountApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<AccountViewModel>> GetAccounts(string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync("api/accounts");

            if (!response.IsSuccessStatusCode)
                return new List<AccountViewModel>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<AccountViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}