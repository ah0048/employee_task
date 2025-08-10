using employees_system.ViewModels.Properties;
using System.ComponentModel.DataAnnotations;

namespace employees_system.ViewModels.Employees
{
    public class CreateEmployeeViewModel
    {
        [Required] 
        public string Code { get; set; }
        [Required] 
        public string Name { get; set; }
        public ICollection<PropertyInputViewModel>? Properties { get; set; }
    }
}
