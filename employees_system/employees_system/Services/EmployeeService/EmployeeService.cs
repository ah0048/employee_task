using AutoMapper;
using employees_system.Models;
using employees_system.UnitOfWorks;
using employees_system.ViewModels.Employees;

namespace employees_system.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public EmployeeService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(createEmployeeViewModel.Code))
                validationErrors.Add("Employee Code is required.");

            if (string.IsNullOrWhiteSpace(createEmployeeViewModel.Name))
                validationErrors.Add("Employee Name is required.");

            if (!string.IsNullOrWhiteSpace(createEmployeeViewModel.Code))
            {
                bool codeExists = await _unit.EmployeeRepo.IsDuplicateCode(createEmployeeViewModel.Code);
                if (codeExists)
                    validationErrors.Add($"Employee Code '{createEmployeeViewModel.Code}' already exists.");
            }

            if (!string.IsNullOrWhiteSpace(createEmployeeViewModel.Name))
            {
                bool nameExists = await _unit.EmployeeRepo.IsDuplicateName(createEmployeeViewModel.Name);
                if (nameExists)
                    validationErrors.Add($"Employee Name '{createEmployeeViewModel.Name}' already exists.");
            }

            var requiredProps = await _unit.PropertyDefinitionRepo.GetAllRequiredAsync();
            foreach (var requiredProp in requiredProps)
            {
                var providedProp = createEmployeeViewModel.Properties?
                    .FirstOrDefault(p => p.PropertyDefinitionId == requiredProp.Id);

                if (providedProp == null || string.IsNullOrWhiteSpace(providedProp.Value))
                {
                    validationErrors.Add($"Property '{requiredProp.Name}' is required.");
                }
            }

            if (validationErrors.Any())
                return ServiceResult.CreateValidationErrors(validationErrors);

            try
            {
                var newEmployee = new Employee
                {
                    Code = createEmployeeViewModel.Code,
                    Name = createEmployeeViewModel.Name
                };

                await _unit.EmployeeRepo.AddAsync(newEmployee);
                await _unit.SaveAsync();

                if (createEmployeeViewModel.Properties?.Any() == true)
                {
                    var employeeProperties = createEmployeeViewModel.Properties
                        .Where(p => !string.IsNullOrWhiteSpace(p.Value))
                        .Select(p => new EmployeeProperty
                        {
                            EmployeeId = newEmployee.Id,
                            PropertyDefinitionId = p.PropertyDefinitionId,
                            Value = p.Value.Trim()
                        })
                        .ToList();

                    if (employeeProperties.Any())
                    {
                        await _unit.EmployeePropertyRepo.AddRangeAsync(employeeProperties);
                        await _unit.SaveAsync();
                    }
                }

                return ServiceResult.CreateSuccess();
            }
            catch (Exception ex)
            {
                return ServiceResult.CreateError($"An error occurred while adding the employee: {ex?.InnerException?.Message}");
            }
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
