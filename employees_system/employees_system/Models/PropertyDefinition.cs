using employees_system.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace employees_system.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class PropertyDefinition
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public PropertyType Type { get; set; }
        [Required]
        public bool IsRequired { get; set; }
        public virtual ICollection<PropertyOption> Options { get; set; }
    }
}
