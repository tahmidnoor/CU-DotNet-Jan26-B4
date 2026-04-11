using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Controllers
{
    public class LaptopController : Controller
    {
        private readonly LaptopService _service;

        public LaptopController(LaptopService service)
        {
            _service = service;
        }

        // GET: /Laptop
        public async Task<IActionResult> Index()
        {
            var laptops = await _service.GetAsync();
            return View(laptops);
        }

        // GET: /Laptop/Create
        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(Laptop laptop)
        //{
        //    if (!ModelState.IsValid)
        //        return View(laptop);

        //    await _service.CreateAsync(laptop);

        //    // 👇 ADD THIS
        //    TempData["SuccessMessage"] = "Laptop added successfully!";

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public async Task<IActionResult> Create(Laptop laptop)
        {


            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model Invalid");
                return View(laptop);
            }

          

            await _service.CreateAsync(laptop);


            TempData["SuccessMessage"] = "Laptop added successfully!";
            return RedirectToAction("Index");
        }
    }

}