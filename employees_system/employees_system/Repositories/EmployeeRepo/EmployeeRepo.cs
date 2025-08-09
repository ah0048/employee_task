using employees_system.Models;
using Microsoft.EntityFrameworkCore;

namespace employees_system.Repositories.EmployeeRepo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _db;
        public EmployeeRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Employee obj)
        {
            await _db.Employees.AddAsync(obj);
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _db.Employees.OrderBy(e => e.Code).ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
            }
        }

        public Task EditAsync(Employee obj)
        {
            if (obj != null)
            {
                _db.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }
    }
}
