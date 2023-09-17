using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class EmployeeRequest
    {
        public Guid? Id { get; set; }

        public Title Title { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

    }
}
