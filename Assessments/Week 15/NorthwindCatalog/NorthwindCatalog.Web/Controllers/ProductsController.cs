using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Services.DTOs;

namespace NorthwindCatalog.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _client;

        public ProductsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> ByCategory(int id)
        {
            var data = await _client.GetFromJsonAsync<List<ProductDto>>
                ($"api/products/by-category/{id}");

            return View(data);
        }
    }
}