using employees_system.Models.Enums;

namespace employees_system.ViewModels.Properties
{
    public class PropertyDefinitionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PropertyType Type { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; }
    }
}
