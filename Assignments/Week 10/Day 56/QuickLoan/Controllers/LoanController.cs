using Microsoft.AspNetCore.Mvc;
using QuickLoan.Models;

public class LoanController : Controller
{
    private static List<Loan> loans = new List<Loan>();

    public IActionResult Index()
    {
        return View(loans);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Loan loan)
    {
        if (ModelState.IsValid)
        {
            loan.Id = loans.Count + 1;
            loans.Add(loan);
            return RedirectToAction("Index");
        }
        return View(loan);
    }

    public IActionResult Edit(int id)
    {
        var loan = loans.FirstOrDefault(x => x.Id == id);
        return View(loan);
    }

    [HttpPost]
    public IActionResult Edit(Loan updatedLoan)
    {
        var loan = loans.FirstOrDefault(x => x.Id == updatedLoan.Id);

        loan.BorrowerName = updatedLoan.BorrowerName;
        loan.LenderName = updatedLoan.LenderName;
        loan.Amount = updatedLoan.Amount;
        loan.IsSettled = updatedLoan.IsSettled;

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var loan = loans.FirstOrDefault(x => x.Id == id);
        loans.Remove(loan);
        return RedirectToAction("Index");
    }
}