using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Services;

namespace SmartBank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountApiService _accountService;

        public HomeController(AccountApiService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
                return View("Landing");

            var accounts = await _accountService.GetAccounts(token);

            return View("Dashboard", accounts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}