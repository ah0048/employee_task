using employees_system.Services.EmployeeService;
using employees_system.Services.PropertyService;
using employees_system.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace employees_system.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.getAllEmployees();
            return View("Index", employees);
        }

        public IActionResult NewEmployee()
        {
            return View("AddNewEmployee");
        }
        public async Task<IActionResult> AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddNewEmployee(createEmployeeViewModel);
                return RedirectToAction("Index");
            }
            return View("AddNewEmployee", createEmployeeViewModel);
        }


    }
}
