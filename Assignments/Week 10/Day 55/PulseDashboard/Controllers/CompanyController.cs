using Microsoft.AspNetCore.Mvc;
using PulseDashboard.Models;


namespace PulseDashboard.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee{Id = 1,Name="Shreya Biswas",Position="Developer",Salary =60000},
                new Employee{Id = 2,Name="Deepshikha Kumari",Position="Analyst",Salary = 75000},
                new Employee{Id = 3,Name="Parth Vats",Position="Manager",Salary =50000},
                new Employee{Id = 4,Name="Raj Satyam",Position="QA Engineer",Salary =65000},
            };

            ViewBag.Annoucement = "Meeting @ 4pm today ";
            ViewData["DepartmentName"] = "IT Department";
            ViewData["ServerStatus"] = true;

            return View(employees);
        }
    }
}
