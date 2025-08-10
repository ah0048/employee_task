using employees_system.ViewModels.Properties;

namespace employees_system.Services.PropertyService
{
    public interface IPropertyService
    {
        Task AddNewProperty(CreatePropertyViewModel createPropertyViewModel);
    }
}
