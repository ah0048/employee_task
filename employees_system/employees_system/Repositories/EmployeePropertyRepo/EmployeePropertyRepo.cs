using employees_system.Models;
using Microsoft.EntityFrameworkCore;

namespace employees_system.Repositories.EmployeePropertyRepo
{
    public class EmployeePropertyRepo : IEmployeePropertyRepo
    {
        private readonly AppDbContext _db;
        public EmployeePropertyRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(EmployeeProperty obj)
        {
            await _db.EmployeeProperties.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            var employeeProperty = await GetByIdAsync(id);
            if (employeeProperty != null)
            {
                _db.EmployeeProperties.Remove(employeeProperty);
            }
        }

        public Task EditAsync(EmployeeProperty obj)
        {
            if (obj != null)
            {
                _db.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }

        public async Task<List<EmployeeProperty>> GetAllAsync()
        {
            return await _db.EmployeeProperties.ToListAsync();
        }

        public async Task<EmployeeProperty> GetByIdAsync(int id)
        {
            return await _db.EmployeeProperties.FindAsync(id);
        }
        public async Task AddRangeAsync(List<EmployeeProperty> properties)
        {
            await _db.EmployeeProperties.AddRangeAsync(properties);
        }

        public Task UpdateRangeAsync(List<EmployeeProperty> properties)
        {
            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    _db.Entry(prop).State = EntityState.Modified;
                }
            }
            return Task.CompletedTask;
        }
        public async Task<List<EmployeeProperty>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _db.EmployeeProperties.Where(ep => ep.EmployeeId == employeeId).ToListAsync();
        }
        public async Task DeleteByEmployeeIdAsync(int employeeId)
        {
            var employeeProperty = await _db.EmployeeProperties.FirstOrDefaultAsync(ep=> ep.EmployeeId == employeeId);
            if (employeeProperty != null)
            {
                _db.EmployeeProperties.Remove(employeeProperty);
            }
        }

        public async Task<List<EmployeeProperty>> GetByPropertyDefinitionIdAsync(int propertyDefId)
        {
            return await _db.EmployeeProperties.Where(ep => ep.PropertyDefinitionId == propertyDefId).ToListAsync();
        }
    }
}

