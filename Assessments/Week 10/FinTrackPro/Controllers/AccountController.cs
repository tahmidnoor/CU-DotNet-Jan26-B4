using FinTrackPro.Data;
using FinTrackPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTrackPro.Controllers
{
    public class AccountController : Controller
    {
        private readonly DContext _context;

        public AccountController(DContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var accounts = _context.Accounts
                .Include(a => a.Transactions)
                .ToList();

            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();

                TempData["Success"] = "Account created successfully";
                return RedirectToAction("Index");
            }

            return View(account);
        }
    }
}