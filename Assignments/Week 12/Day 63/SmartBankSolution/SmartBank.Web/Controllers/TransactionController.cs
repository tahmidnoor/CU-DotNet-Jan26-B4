using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Services;

namespace SmartBank.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionApiService _service;

        public TransactionController(TransactionApiService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(int accountId, decimal amount)
        {
            var token = HttpContext.Session.GetString("JWToken");

            var success = await _service.Deposit(accountId, amount, token);

            return Json(new { success, message = success ? "" : "Deposit failed" });
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(int accountId, decimal amount)
        {
            var token = HttpContext.Session.GetString("JWToken");

            var success = await _service.Withdraw(accountId, amount, token);

            return Json(new { success, message = success ? "" : "Withdraw failed" });
        }
    }
}