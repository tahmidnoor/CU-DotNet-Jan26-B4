using FinTrackPro.Data;
using FinTrackPro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinTrackPro.Controllers
{
    public class TransactionController : Controller
    {
        private readonly DContext _context;

        public TransactionController(DContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var transactions = _context.Transactions
                .Include(t => t.Account)
                .ToList();

            return View(transactions);
        }

        public IActionResult Create()
        {
            ViewBag.AccountList = new SelectList(
                _context.Accounts.ToList(),
                "AccountID",
                "AccountName"
            );

            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                TempData["Success"] = "Account created successfully";
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();

                TempData["Message"] = "Transaction deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}