using employees_system.Models;

namespace employees_system.Repositories.EmployeeRepo
{
    public interface IEmployeeRepo: IRepository<Employee>
    {
        Task<List<Employee>> GetAllWithPropertiesAsync();
        Task<bool> IsDuplicateCode(string code);
        Task<bool> IsDuplicateName(string name);
    }
}
