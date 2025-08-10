using AutoMapper;
using employees_system.Models;
using employees_system.Models.Enums;
using employees_system.UnitOfWorks;
using employees_system.ViewModels.Properties;
using Microsoft.EntityFrameworkCore;

namespace employees_system.Services.PropertyService
{
    public class PropertyService: IPropertyService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PropertyService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddNewProperty(CreatePropertyViewModel createPropertyViewModel)
        {
            try
            {
                var existingProperties = await _unit.PropertyDefinitionRepo.GetAllAsync();
                if (existingProperties.Any(p => p.Name.Equals(createPropertyViewModel.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    return ServiceResult.CreateValidationError($"A property with the name '{createPropertyViewModel.Name}' already exists.");
                }

                if (createPropertyViewModel.Type == PropertyType.Dropdown)
                {
                    if (string.IsNullOrWhiteSpace(createPropertyViewModel.DropdownOptionsCommaSeparated))
                    {
                        return ServiceResult.CreateValidationError("Dropdown properties must have at least one option.");
                    }

                    var options = createPropertyViewModel.DropdownOptionsCommaSeparated
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.Trim())
                        .Where(o => !string.IsNullOrWhiteSpace(o))
                        .ToList();

                    if (!options.Any())
                    {
                        return ServiceResult.CreateValidationError("Dropdown properties must have at least one valid option.");
                    }
                }

                var propertyDef = _mapper.Map<PropertyDefinition>(createPropertyViewModel);
                await _unit.PropertyDefinitionRepo.AddAsync(propertyDef);
                
                if (propertyDef.Type == PropertyType.Dropdown && createPropertyViewModel.DropdownOptionsCommaSeparated != null)
                {
                    var propertyOptionsList = createPropertyViewModel
                        .DropdownOptionsCommaSeparated
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.Trim())
                        .Where(o => !string.IsNullOrWhiteSpace(o))
                        .Select(o => char.ToUpper(o[0]) + o.Substring(1).ToLower())
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    propertyDef.Options = propertyOptionsList
                        .Select(option => new PropertyOption { Value = option })
                        .ToList();
                }
                
                await _unit.SaveAsync();
                return ServiceResult.CreateSuccess();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate") == true || 
                                            ex.InnerException?.Message.Contains("unique") == true ||
                                            ex.InnerException?.Message.Contains("UNIQUE") == true)
            {
                return ServiceResult.CreateValidationError($"A property with the name '{createPropertyViewModel.Name}' already exists.");
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateError("An unexpected error occurred while creating the property. Please try again.");
            }
        }

        public async Task<List<ShowPropertyViewModel>> GetAllProperties()
        {
            var properties = await _unit.PropertyDefinitionRepo.GetAllWithPropertiesOptionsAsync();
            List<ShowPropertyViewModel> showPropertyViewModels = new List<ShowPropertyViewModel>();
            foreach (var property in properties)
            {
                ShowPropertyViewModel showPropertyViewModel = new ShowPropertyViewModel
                {
                    Name = property.Name,
                    Required = property.IsRequired.ToString(),
                    Type = property.Type.ToString(),
                    options = string.Join(", ", property.Options.Select(o => o.Value))
                };
                showPropertyViewModels.Add(showPropertyViewModel);
            }
            return showPropertyViewModels;
        }

        public async Task<List<PropertyDefinitionViewModel>> GetAllDefinitionsWithOptionsAsync()
        {
            var properties = await _unit.PropertyDefinitionRepo.GetAllWithPropertiesOptionsAsync();
            return properties.Select(pd => new PropertyDefinitionViewModel
            {
                Id = pd.Id,
                Name = pd.Name,
                Type = pd.Type,
                IsRequired = pd.IsRequired,
                Options = pd.Options?.Select(o => o.Value).ToList() ?? new List<string>()
            }).ToList();
        }
    }
}
