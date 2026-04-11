using Microsoft.AspNetCore.Mvc;
using SmartBank.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SmartBank.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // 🔥 FIX: Use ClaimTypes.Name
            var email = jwtToken.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var model = new ProfileViewModel
            {
                Email = email
            };

            return View(model);
        }
    }
}