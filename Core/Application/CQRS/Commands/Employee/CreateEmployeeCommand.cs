using Domain.Enums;
using MediatR;

namespace Application.CQRS.Commands.Employee
{
    public sealed record CreateEmployeeCommand(Title Title, string Email) : IRequest<Domain.Entities.Employee>
    {
    }
}
