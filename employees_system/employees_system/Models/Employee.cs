namespace employees_system.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EmployeeProperty> Properties { get; set; }
    }
}
