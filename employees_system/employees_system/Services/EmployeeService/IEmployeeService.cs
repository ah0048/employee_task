using employees_system.Models;
using employees_system.ViewModels.Employees;

namespace employees_system.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResult> AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel);
        Task<EmployeeTableData> getAllEmployees();
    }
}
