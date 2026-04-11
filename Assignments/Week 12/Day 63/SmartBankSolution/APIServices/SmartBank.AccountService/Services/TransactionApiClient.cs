using SmartBank.AccountService.DTOs;
using SmartBank.AccountService.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartBank.AccountService.Services
{
    public class TransactionApiClient : ITransactionApiClient
    {
        private readonly HttpClient _client;

        public TransactionApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateTransaction(TransactionCreateDto dto, string token)
        {
            // create request and set Authorization per-request to avoid mutating shared HttpClient headers
            var request = new HttpRequestMessage(HttpMethod.Post, "api/transactions")
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(dto),
                    Encoding.UTF8,
                    "application/json")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // throw new Exception("Transaction service failed");
                var error = await response.Content.ReadAsStringAsync();

                throw new ApplicationException(
                    $"Transaction service failed: {response.StatusCode}, {error}");
            }
        }

        public async Task<List<TransactionDto>> GetTransactionsByAccountId(int accountId, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/transactions/account/{accountId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new ApplicationException(
                    $"Failed to fetch transactions: {response.StatusCode}, {error}");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<TransactionDto>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
