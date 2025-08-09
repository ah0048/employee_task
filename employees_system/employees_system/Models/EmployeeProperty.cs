using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employees_system.Models
{
    public class EmployeeProperty
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(PropertyDefinition))]
        public int PropertyDefinitionId { get; set; }
        [Required]
        public string Value { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual PropertyDefinition PropertyDefinition { get; set; }
    }
}
