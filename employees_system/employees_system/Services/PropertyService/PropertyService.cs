using AutoMapper;
using employees_system.Models;
using employees_system.Models.Enums;
using employees_system.UnitOfWorks;
using employees_system.ViewModels.Properties;

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

        public async Task AddNewProperty(CreatePropertyViewModel createPropertyViewModel)
        {
            try
            {
                var propertyDef = _mapper.Map<PropertyDefinition>(createPropertyViewModel);
                await _unit.PropertyDefinitionRepo.AddAsync(propertyDef);
                if (propertyDef.Type == PropertyType.Dropdown && createPropertyViewModel.DropdownOptionsCommaSeparated != null)
                {
                    var propertyOptionsList = createPropertyViewModel
                        .DropdownOptionsCommaSeparated
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.Trim())
                        .Select(o => char.ToUpper(o[0]) + o.Substring(1).ToLower())
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    propertyDef.Options = propertyOptionsList
                        .Select(option => new PropertyOption { Value = option })
                        .ToList();
                }
                await _unit.SaveAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
