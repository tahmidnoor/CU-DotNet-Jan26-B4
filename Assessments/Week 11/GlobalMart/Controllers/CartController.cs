using Microsoft.AspNetCore.Mvc;
using GlobalMart.Services;

namespace GlobalMart.Controllers
{
    public class CartController : Controller
    {
        private readonly IPricingService _pricingService;

        public CartController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewBag.Subtotal = 0.00m;
            ViewBag.PromoCode = string.Empty;
            ViewBag.FinalTotal = 0.00m;
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(decimal subtotal, string promoCode)
        {
            var finalTotal = _pricingService.CalculatePrice(subtotal, promoCode);
            ViewBag.Subtotal = subtotal;
            ViewBag.PromoCode = promoCode ?? string.Empty;
            ViewBag.FinalTotal = finalTotal;
            return View();
        }
    }
}