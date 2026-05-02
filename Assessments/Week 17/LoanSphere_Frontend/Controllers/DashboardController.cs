using LoanSphere_Frontend.Models;
using LoanSphere_Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanSphere_Frontend.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CurrentUserService _currentUserService;
        private readonly LoanSphereApiClient _apiClient;

        public DashboardController(
            CurrentUserService currentUserService,
            LoanSphereApiClient apiClient)
        {
            _currentUserService = currentUserService;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var user = _currentUserService.GetCurrentUser();

            if (user == null)
            {
                TempData["ErrorMessage"] = "Please log in to continue.";
                return RedirectToAction("Login", "Account");
            }

            var loadErrors = new List<string>();

            // 🔹 PROFILE (for all users)
            var profileResult = await _apiClient.GetProfileAsync(user.UserId, user.Token);

            if (!profileResult.Success)
            {
                loadErrors.Add(profileResult.Message);
            }

            // 🔹 VIEW MODEL BASE
            var viewModel = new DashboardViewModel
            {
                CurrentUser = user,
                Profile = profileResult.Data,
                MyLoans = new List<LoanSummaryViewModel>(),
                ManagedLoans = new List<LoanSummaryViewModel>(),
                RequiresProfileCompletion = !(profileResult.Data?.IsProfileComplete ?? false)
            };

            // =========================
            // 🔥 CUSTOMER FLOW
            // =========================
            if (user.Role == "Customer")
            {
                var myLoansResult = await _apiClient.GetLoansByUserAsync(user.UserId, user.Token);

                // 👉 Only show error if it's NOT "no loans"
                if (!myLoansResult.Success &&
                    !myLoansResult.Message.Contains("No loans", StringComparison.OrdinalIgnoreCase))
                {
                    loadErrors.Add(myLoansResult.Message);
                }

                viewModel.MyLoans = myLoansResult.Data ?? new List<LoanSummaryViewModel>();
            }

            // =========================
            // 🔥 ADMIN / MANAGER FLOW
            // =========================
            if (viewModel.CanManageLoans)
            {
                var allLoansResult = await _apiClient.GetAllLoansAsync(user.Token);

                if (!allLoansResult.Success)
                {
                    loadErrors.Add(allLoansResult.Message);
                }

                viewModel.ManagedLoans = allLoansResult.Data ?? new List<LoanSummaryViewModel>();
            }

            // =========================
            // 🔹 GLOBAL ERROR HANDLING
            // =========================
            if (loadErrors.Count > 0)
            {
                TempData["ErrorMessage"] = string.Join(" ", loadErrors.Distinct());
            }

            return View(viewModel);
        }
    }
}