using employees_system.Models;

namespace employees_system.Repositories.EmployeeRepo
{
    public interface IEmployeeRepo: IRepository<Employee>
    {
        Task<List<Employee>> GetAllWithPropertiesAsync();
    }
}
