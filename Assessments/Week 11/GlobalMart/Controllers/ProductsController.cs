using Microsoft.AspNetCore.Mvc;
using GlobalMart.Services;
using GlobalMart.Models;
using System.Collections.Generic;
using System.Linq;

namespace GlobalMart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPricingService _pricingService;

        public ProductsController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        private static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Rice", Price = 50.00m },
                new Product { Id = 2, Name = "Coffee", Price = 85.00m },
                new Product { Id = 3, Name = "Tea", Price = 45.00m },
                new Product { Id = 4, Name = "Butter", Price = 72.00m },
                new Product { Id = 5, Name = "Biscuits", Price = 40.00m }
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = GetSampleProducts();

            ViewBag.Subtotal = 0.00m;
            ViewBag.DiscountedTotal = 0.00m;
            ViewBag.PromoCode = string.Empty;

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int[]? productIds, string? promoCode)
        {
            var products = GetSampleProducts();

            decimal subtotal = 0.00m;
            if (productIds != null && productIds.Length > 0)
            {
                subtotal = products
                    .Where(p => productIds.Contains(p.Id))
                    .Sum(p => p.Price);
            }

            var discounted = _pricingService.CalculatePrice(subtotal, promoCode ?? string.Empty);

            ViewBag.Subtotal = subtotal;
            ViewBag.DiscountedTotal = discounted;
            ViewBag.PromoCode = promoCode ?? string.Empty;

            return View(products);
        }
    }
}