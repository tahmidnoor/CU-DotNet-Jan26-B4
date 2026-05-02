using LoanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LoanSphere_Frontend.Controllers
{
    public class EMIController : Controller
    {
        private readonly HttpClient _http;

        public EMIController(HttpClient http)
        {
            _http = http;
        }

        // 🔹 PAYMENT PAGE (GET)
        [HttpGet]
        public async Task<IActionResult> Payments(int emiId, int loanId)
        {
            var token = HttpContext.Session.GetString("CurrentUser.Token");

            // 🔥 Attach JWT
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.GetAsync(
                $"https://localhost:7002/loan/getbyid/{loanId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to fetch loan";
                return RedirectToAction("Index", "Dashboard");
            }

            var loan = await response.Content.ReadFromJsonAsync<Loan>();

            if (loan == null)
            {
                TempData["Error"] = "Loan not found";
                return RedirectToAction("Index", "Dashboard");
            }

            var emi = loan.EMISchedules?.FirstOrDefault(e => e.Id == emiId);

            if (emi == null)
            {
                TempData["Error"] = "EMI not found";
                return RedirectToAction("Index", "Dashboard");
            }

            return View("Pay", emi);
        }

        // 🔥 PROCESS PAYMENT (POST)
        [HttpPost]
        public async Task<IActionResult> Pay(int emiId, int loanId)
        {
            var token = HttpContext.Session.GetString("CurrentUser.Token");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _http.PostAsync(
                $"https://localhost:7002/loan/pay-emi/{emiId}",
                null);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Payment successful!";

                return RedirectToAction("Details", "LoanUI", new
                {
                    id = loanId
                });
            }

            TempData["Error"] = "Payment failed!";
            return RedirectToAction("Payments", new { emiId, loanId });
        }
    }
}