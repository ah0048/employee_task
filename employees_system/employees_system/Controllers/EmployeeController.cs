using Microsoft.AspNetCore.Mvc;

namespace employees_system.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
