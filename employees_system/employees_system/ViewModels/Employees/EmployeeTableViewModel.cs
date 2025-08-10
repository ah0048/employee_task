namespace employees_system.ViewModels.Employees
{
    public class EmployeeTableViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string?> PropertyValues { get; set; }
    }
}
