using Microsoft.AspNetCore.Mvc;

namespace employees_system.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
