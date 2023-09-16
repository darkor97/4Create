using Domain.Enums;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public Title Title { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public IList<Company>? Companies { get; set; }
    }
}
