using employees_system.Models.Enums;

namespace employees_system.Models
{
    public class PropertyDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public virtual ICollection<PropertyOption> Options { get; set; }
    }
}
