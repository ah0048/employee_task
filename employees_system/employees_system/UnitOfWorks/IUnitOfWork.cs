using employees_system.Repositories.EmployeePropertyRepo;
using employees_system.Repositories.EmployeeRepo;
using employees_system.Repositories.PropertyDefinitionRepo;
using employees_system.Repositories.PropertyOptionRepo;

namespace employees_system.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IEmployeeRepo EmployeeRepo { get; }
        IEmployeePropertyRepo EmployeePropertyRepo { get; }
        IPropertyDefinitionRepo PropertyDefinitionRepo { get; }
        IPropertyOptionRepo PropertyOptionRepo { get; }

        Task SaveAsync();
        void Dispose();
    }
}
