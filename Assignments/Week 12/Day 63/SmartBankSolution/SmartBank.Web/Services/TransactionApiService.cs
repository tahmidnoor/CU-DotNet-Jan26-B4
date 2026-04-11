using SmartBank.Web.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartBank.Web.Services
{
    public class TransactionApiService
    {
        private readonly HttpClient _client;

        public TransactionApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TransactionViewModel>> GetTransactions(int accountId, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"api/transactions/account/{accountId}");

            if (!response.IsSuccessStatusCode)
                return new List<TransactionViewModel>();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TransactionViewModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> Deposit(int accountId, decimal amount, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var data = new
            {
                AccountId = accountId,
                Amount = amount
            };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync($"api/accounts/deposit", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Withdraw(int accountId, decimal amount, string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var data = new
            {
                AccountId = accountId,
                Amount = amount
            };

            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync($"api/accounts/withdraw", content);

            return response.IsSuccessStatusCode;
        }
    }
}