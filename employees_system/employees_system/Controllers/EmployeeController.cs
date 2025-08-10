using employees_system.Services.EmployeeService;
using employees_system.Services.PropertyService;
using employees_system.ViewModels.Employees;
using employees_system.ViewModels.Properties;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace employees_system.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPropertyService _propertyService;

        public EmployeeController(IEmployeeService employeeService, IPropertyService propertyService)
        {
            _employeeService = employeeService;
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.getAllEmployees();
            return View("Index", employees);
        }

        public async Task<IActionResult> NewEmployee()
        {
            var propertyDefs = await _propertyService.GetAllDefinitionsWithOptionsAsync();
            
            var createEmpoloyeeVM = new CreateEmployeeViewModel
            {
                Properties = new List<PropertyInputViewModel>()
            };

            ViewBag.PropertyDefinitions = propertyDefs;

            return View("AddNewEmployee", createEmpoloyeeVM);
        }
        public async Task<IActionResult> AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.AddNewEmployee(createEmployeeViewModel);

                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Employee created successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.ValidationErrors)
                        ModelState.AddModelError("", error);

                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                        ModelState.AddModelError("", result.ErrorMessage);
                }
            }

            var propertyDefs = await _propertyService.GetAllDefinitionsWithOptionsAsync();
            ViewBag.PropertyDefinitions = propertyDefs;

            return View("AddNewEmployee", createEmployeeViewModel);
        }


    }
}
