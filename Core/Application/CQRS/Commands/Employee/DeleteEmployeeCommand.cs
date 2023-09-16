using MediatR;

namespace Application.CQRS.Commands.Employee
{
    public sealed record DeleteEmployeeCommand(Domain.Entities.Employee Employee) : IRequest
    {
    }
}
