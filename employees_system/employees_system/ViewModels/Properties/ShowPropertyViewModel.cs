using employees_system.Models.Enums;

namespace employees_system.ViewModels.Properties
{
    public class ShowPropertyViewModel
    {
        public string Name { get; set; }
        public string Required { get; set; }
        public string Type { get; set; }
        public string? options { get; set; }
    }
}
