namespace Presentation.Models
{
    public class CompanyRequest
    {
        public required string Name { get; set; }

        public IEnumerable<EmployeeRequest>? Employees { get; set; }
    }
}
