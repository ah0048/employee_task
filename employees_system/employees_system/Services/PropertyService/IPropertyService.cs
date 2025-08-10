using employees_system.Models;
using employees_system.ViewModels.Properties;

namespace employees_system.Services.PropertyService
{
    public interface IPropertyService
    {
        Task<ServiceResult> AddNewProperty(CreatePropertyViewModel createPropertyViewModel);
        Task<List<ShowPropertyViewModel>> GetAllProperties();
        Task<List<PropertyDefinitionViewModel>> GetAllDefinitionsWithOptionsAsync();
    }
}
