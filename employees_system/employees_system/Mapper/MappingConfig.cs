using AutoMapper;
using employees_system.Models;
using employees_system.ViewModels.Employees;
using employees_system.ViewModels.Properties;

namespace employees_system.Mapper
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<CreatePropertyViewModel, PropertyDefinition>().ReverseMap();
            CreateMap<CreateEmployeeViewModel, Employee>().ReverseMap();
            CreateMap<PropertyInputViewModel, EmployeeProperty>().ReverseMap();
        }
    }
}
