using Domain.Enums;

namespace Presentation.Models
{
    public class EmployeeRequest
    {
        public Title Title { get; set; }

        public required string Email { get; set; }
    }
}
