using employees_system.Models;
using employees_system.Repositories.EmployeePropertyRepo;
using employees_system.Repositories.EmployeeRepo;
using employees_system.Repositories.PropertyDefinitionRepo;
using employees_system.Repositories.PropertyOptionRepo;
using Microsoft.EntityFrameworkCore;

namespace employees_system.UnitOfWorks
{
    public class UnitOfWork
    {
        private readonly AppDbContext _db;
        private IEmployeePropertyRepo employeePropertyRepo;
        private IEmployeeRepo employeeRepo;
        private IPropertyDefinitionRepo propertyDefinitionRepo;
        private IPropertyOptionRepo propertyOptionRepo;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }
        public IEmployeeRepo EmployeeRepo
        {
            get
            {
                if (employeeRepo == null)
                {
                    employeeRepo = new EmployeeRepo(_db);
                }
                ;
                return employeeRepo;
            }
        }



        public IEmployeePropertyRepo EmployeePropertyRepo
        {
            get
            {
                if (employeePropertyRepo == null)
                {
                    employeePropertyRepo = new EmployeePropertyRepo(_db);
                }
                ;
                return employeePropertyRepo;
            }
        }

        public IPropertyDefinitionRepo PropertyDefinitionRepo
        {
            get
            {
                if (propertyDefinitionRepo == null)
                {
                    propertyDefinitionRepo = new PropertyDefinitionRepo(_db);
                }
                ;
                return propertyDefinitionRepo;
            }
        }
        public IPropertyOptionRepo PropertyOptionRepo
        {
            get
            {
                if (propertyOptionRepo == null)
                {
                    propertyOptionRepo = new PropertyOptionRepo(_db);
                }
                ;
                return propertyOptionRepo;
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
