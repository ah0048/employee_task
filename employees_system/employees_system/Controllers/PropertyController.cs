using employees_system.Models.Enums;
using employees_system.Services.PropertyService;
using employees_system.ViewModels.Properties;
using Microsoft.AspNetCore.Mvc;

namespace employees_system.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        [HttpGet]
        public IActionResult NewProperty()
        {
            return View("AddNewProperty");
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProperty(CreatePropertyViewModel createPropertyViewModel)
        {
            if (ModelState.IsValid)
            {
                if (createPropertyViewModel.Type == PropertyType.Dropdown && string.IsNullOrWhiteSpace(createPropertyViewModel.DropdownOptionsCommaSeparated))
                {
                    ModelState.AddModelError("DropdownOptionsCommaSeparated", "Dropdown properties must have at least one option.");
                    return View("AddNewProperty", createPropertyViewModel);
                }

                var result = await _propertyService.AddNewProperty(createPropertyViewModel);
                
                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Property created successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Add validation errors to ModelState
                    foreach (var error in result.ValidationErrors)
                    {
                        ModelState.AddModelError("", error);
                    }

                    // Add general error if exists
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        ModelState.AddModelError("", result.ErrorMessage);
                    }
                }
            }
            
            return View("AddNewProperty", createPropertyViewModel);
        }
    }
}
