using System.ComponentModel.DataAnnotations.Schema;

namespace employees_system.Models
{
    public class PropertyOption
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PropertyDefinition))]
        public int PropertyDefinitionId { get; set; }
        public string Value { get; set; }
        public virtual PropertyDefinition PropertyDefinition { get; set; }
    }
}
