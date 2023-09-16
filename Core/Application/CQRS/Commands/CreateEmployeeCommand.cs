using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Commands
{
    public sealed record CreateEmployeeCommand(Title Title, string Email) : IRequest<Employee>
    {
    }
}
