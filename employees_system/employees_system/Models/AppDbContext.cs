using Microsoft.EntityFrameworkCore;

namespace employees_system.Models
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeProperty> EmployeeProperties { get; set; }
        public virtual DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public virtual DbSet<PropertyOption> PropertyOptions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
    }
}
