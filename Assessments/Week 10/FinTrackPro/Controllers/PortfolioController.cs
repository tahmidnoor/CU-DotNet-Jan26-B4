using Microsoft.AspNetCore.Mvc;
using FinTrackPro.Models;

namespace FinTrackPro.Controllers
{
    public class PortfolioController : Controller
    {
        private static List<Asset> assets = new List<Asset>()
        {
            new Asset { Id = 1, Name = "Tata", Value = 5000 },
            new Asset { Id = 2, Name = "JIO", Value = 3000 },
            new Asset { Id = 3, Name = "Adani", Value = 4000 }
        };

        public IActionResult Index()
        {
            double total = assets.Sum(a => a.Value);
            ViewData["Total"] = total;

            return View(assets);
        }

        [Route("Asset/Info/{id:int}")]
        public IActionResult Details(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);
            return View(asset);
        }

        public IActionResult Delete(int id)
        {
            var asset = assets.FirstOrDefault(a => a.Id == id);

            if (asset != null)
            {
                assets.Remove(asset);
            }

            TempData["Message"] = "Asset Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}