using employees_system.Models;

namespace employees_system.Repositories.EmployeePropertyRepo
{
    public interface IEmployeePropertyRepo: IRepository<EmployeeProperty>
    {
        Task AddRangeAsync(List<EmployeeProperty> properties);
        Task UpdateRangeAsync(List<EmployeeProperty> properties);
        Task<List<EmployeeProperty>> GetByEmployeeIdAsync(int employeeId);
        Task DeleteByEmployeeIdAsync(int employeeId);
        Task<List<EmployeeProperty>> GetByPropertyDefinitionIdAsync(int propertyDefId);
    }
}
