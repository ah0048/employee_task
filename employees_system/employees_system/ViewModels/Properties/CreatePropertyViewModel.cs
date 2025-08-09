using employees_system.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace employees_system.ViewModels.Properties
{
    public class CreatePropertyViewModel
    {
        [Required] 
        public string Name { get; set; } = null!;
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public string? DropdownOptionsCommaSeparated { get; set; }
    }
}
