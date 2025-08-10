using employees_system.ViewModels.Employees;

namespace employees_system.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task AddNewEmployee(CreateEmployeeViewModel createEmployeeViewModel);
    }
}
