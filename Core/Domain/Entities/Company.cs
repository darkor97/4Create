namespace Domain.Entities
{
    public class Company : BaseEntity
    {
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public IList<Employee>? Employees { get; set; }
    }
}
