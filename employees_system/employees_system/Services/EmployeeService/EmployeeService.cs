using AutoMapper;
using employees_system.Models;
using employees_system.UnitOfWorks;
using employees_system.ViewModels.Employees;

namespace employees_system.Services.EmployeeService
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public EmployeeService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel)
        {
            var newEmployee = _mapper.Map<Employee>(createEmployeeViewModel);
            await _unit.EmployeeRepo.AddAsync(newEmployee);
            await _unit.SaveAsync();
        }

        public async Task<EmployeeTableData> getAllEmployees()
        {
            var propertyDefinitions = await _unit.PropertyDefinitionRepo.GetAllAsync();

            var employees = await _unit.EmployeeRepo.GetAllWithPropertiesAsync();

            var employeeTable = new EmployeeTableData
            {
                PropertyHeaders = propertyDefinitions.Select(pd => pd.Name).ToList(),
                Employees = employees.Select(emp => new EmployeeTableViewModel
                {
                    Id = emp.Id,
                    Code = emp.Code,
                    Name = emp.Name,
                    PropertyValues = propertyDefinitions.Select(pd =>
                    {
                        var prop = emp.Properties.FirstOrDefault(ep => ep.PropertyDefinitionId == pd.Id);
                        return prop?.Value;
                    }).ToList()
                }).ToList()
            };

            return employeeTable;
        }
    }
}
