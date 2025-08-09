using System.ComponentModel.DataAnnotations;

namespace employees_system.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<EmployeeProperty> Properties { get; set; }
    }
}
