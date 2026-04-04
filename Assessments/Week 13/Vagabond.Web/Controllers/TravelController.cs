using Microsoft.AspNetCore.Mvc;
using Vagabond.Web.Models;
using Vagabond.Web.Services;

namespace Vagabond.Web.Controllers
{
    public class TravelController : Controller
    {
        private readonly IDestinationService _service;

        public TravelController(IDestinationService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();

            // 🔥 SORT by LastVisited (latest first)
            var sortedData = data
                .OrderByDescending(d => d.LastVisited)
                .ToList();

            return View(sortedData);
        }
    }
}