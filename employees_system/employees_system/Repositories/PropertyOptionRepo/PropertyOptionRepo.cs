using employees_system.Models;
using Microsoft.EntityFrameworkCore;

namespace employees_system.Repositories.PropertyOptionRepo
{
    public class PropertyOptionRepo : IPropertyOptionRepo
    {
        private readonly AppDbContext _db;
        public PropertyOptionRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(PropertyOption obj)
        {
            await _db.PropertyOptions.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            var propertyOption = await GetByIdAsync(id);
            if (propertyOption != null)
            {
                _db.PropertyOptions.Remove(propertyOption);
            }
        }

        public Task EditAsync(PropertyOption obj)
        {
            if (obj != null)
            {
                _db.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }

        public async Task<List<PropertyOption>> GetAllAsync()
        {
            return await _db.PropertyOptions.ToListAsync();
        }

        public async Task<PropertyOption> GetByIdAsync(int id)
        {
            return await _db.PropertyOptions.FindAsync(id);
        }
    }
}
