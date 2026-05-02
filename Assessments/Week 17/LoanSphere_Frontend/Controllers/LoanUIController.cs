using LoanSphere_Frontend.Models;
using LoanSphere_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanSphere_Frontend.Controllers
{
    public class LoanUIController : Controller
    {
        private readonly CurrentUserService _currentUserService;
        private readonly LoanSphereApiClient _apiClient;

        public LoanUIController(CurrentUserService currentUserService, LoanSphereApiClient apiClient)
        {
            _currentUserService = currentUserService;
            _apiClient = apiClient;
        }

        // =========================
        // APPLY (GET)
        // =========================
        [HttpGet]
        public async Task<IActionResult> Apply()
        {
            var user = _currentUserService.GetCurrentUser();
            if (user == null)
            {
                TempData["ErrorMessage"] = "Please log in to apply for a loan.";
                return RedirectToAction("Login", "Account");
            }

            var profileResult = await _apiClient.GetProfileAsync(user.UserId, user.Token);
            if (!profileResult.Success || profileResult.Data == null)
            {
                TempData["ErrorMessage"] = "Unable to load your profile right now.";
                return RedirectToAction("Index", "Dashboard");
            }

            if (!profileResult.Data.IsProfileComplete)
            {
                TempData["ErrorMessage"] = "Please complete your profile before applying for a loan.";
                return RedirectToAction("Edit", "Profile");
            }

            return View(new LoanApplicationViewModel());
        }

        // =========================
        // APPLY (POST) 🔥 UPDATED
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(LoanApplicationViewModel model, IFormFile document)
        {
            var user = _currentUserService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var profileResult = await _apiClient.GetProfileAsync(user.UserId, user.Token);
            if (!profileResult.Success || profileResult.Data == null || !profileResult.Data.IsProfileComplete)
            {
                TempData["ErrorMessage"] = "Please complete your profile before applying for a loan.";
                return RedirectToAction("Edit", "Profile");
            }

            // 🔥 FILE VALIDATION
            if (document == null || document.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a document.");
                return View(model);
            }

            if (!document.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Only PDF files are allowed.");
                return View(model);
            }

            // 🔥 SAVE FILE
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(document.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await document.CopyToAsync(stream);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 🔥 PASS FILE NAME TO API
            var result = await _apiClient.ApplyLoanAsync(user, model, user.Token, fileName);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempData["StatusMessage"] = "Loan application submitted successfully. It is now pending review.";
            return RedirectToAction("Index", "Dashboard");
        }

        // =========================
        // DETAILS
        // =========================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = _currentUserService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _apiClient.GetLoanByIdAsync(id, user.Token);
            if (!result.Success || result.Data == null)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index", "Dashboard");
            }

            var loan = result.Data;

            if (user.Role == "Customer" && loan.UserId != user.UserId)
            {
                TempData["ErrorMessage"] = "You do not have access to that loan.";
                return RedirectToAction("Index", "Dashboard");
            }

            ProfileViewModel? profile = null;

            // 🔥 Only Admin / Manager can fetch profile
            if (user.Role == "Admin" || user.Role == "Manager")
            {
                var profileResult = await _apiClient.GetProfileAsync(loan.UserId, user.Token);

                if (profileResult.Success)
                {
                    profile = profileResult.Data;
                }
            }

            var viewModel = new LoanDetailsViewModel
            {
                CurrentUser = user,
                Loan = loan,
                Profile = profile
            };

            return View(viewModel);
        }

        // =========================
        // UPDATE DECISION
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDecision(int id, string status, string? reason)
        {
            var user = _currentUserService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (user.Role is not ("Admin" or "Manager"))
            {
                TempData["ErrorMessage"] = "Only admins and managers can review loans.";
                return RedirectToAction("Details", new { id });
            }

            if (status == "Rejected" && string.IsNullOrWhiteSpace(reason))
            {
                TempData["ErrorMessage"] = "Please add a rejection reason before rejecting the loan.";
                return RedirectToAction("Details", new { id });
            }

            var result = await _apiClient.UpdateLoanDecisionAsync(id, user.Role, status, reason, user.Token);

            TempData[result.Success ? "StatusMessage" : "ErrorMessage"] = result.Success
                ? $"{user.Role} review submitted as {status}."
                : result.Message;

            return RedirectToAction("Details", new { id });
        }
    }
}