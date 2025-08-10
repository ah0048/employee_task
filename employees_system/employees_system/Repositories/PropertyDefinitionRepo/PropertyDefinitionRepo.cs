using employees_system.Models;
using Microsoft.EntityFrameworkCore;

namespace employees_system.Repositories.PropertyDefinitionRepo
{
    public class PropertyDefinitionRepo : IPropertyDefinitionRepo
    {
        private readonly AppDbContext _db;
        public PropertyDefinitionRepo(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(PropertyDefinition obj)
        {
            await _db.PropertyDefinitions.AddAsync(obj);
        }

        public async Task DeleteAsync(int id)
        {
            var propertyDefinition = await GetByIdAsync(id);
            if (propertyDefinition != null)
            {
                _db.PropertyDefinitions.Remove(propertyDefinition);
            }
        }

        public Task EditAsync(PropertyDefinition obj)
        {
            if (obj != null)
            {
                _db.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
            }
            return Task.CompletedTask;
        }

        public async Task<List<PropertyDefinition>> GetAllAsync()
        {
            return await _db.PropertyDefinitions.OrderBy(p=> p.Id).ToListAsync();
        }

        public async Task<PropertyDefinition> GetByIdAsync(int id)
        {
            return await _db.PropertyDefinitions.FindAsync(id);
        }

        public async Task<List<PropertyDefinition>> GetAllWithPropertiesOptionsAsync()
        {
            return await _db.PropertyDefinitions
                .OrderBy(p=> p.Id)
                .Include(p => p.Options)
                .ToListAsync();
        }

        public async Task<List<PropertyDefinition>> GetAllRequiredAsync()
        {
            return await _db.PropertyDefinitions.Where(p => p.IsRequired).ToListAsync();
        }

    }
}
