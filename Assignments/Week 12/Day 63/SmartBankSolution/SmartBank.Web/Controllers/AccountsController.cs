using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Models;
using SmartBank.Web.Services;

namespace SmartBank.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountApiService _accountService;
        private readonly TransactionApiService _transactionService;

        public AccountsController(
            AccountApiService accountService,
            TransactionApiService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var accounts = await _accountService.GetAccounts(token);

            if (accounts == null)
                accounts = new List<AccountViewModel>();


            // 🔥 Fetch transactions for each account
            foreach (var acc in accounts)
            {
                acc.Transactions = await _transactionService.GetTransactions(acc.Id, token);
            }

            return View(accounts);
        }
    }
}