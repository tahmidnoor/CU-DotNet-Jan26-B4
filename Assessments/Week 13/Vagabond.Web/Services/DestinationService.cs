using System.Text.Json;
using Vagabond.Web.Models;

namespace Vagabond.Web.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _httpClient;

        public DestinationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Destination>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/destinations");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Destination>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}