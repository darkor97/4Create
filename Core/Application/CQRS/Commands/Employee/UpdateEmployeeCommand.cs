using MediatR;

namespace Application.CQRS.Commands.Employee
{
    public sealed record UpdateEmployeeCommand(Domain.Entities.Employee Employee) : IRequest<Domain.Entities.Employee>
    {
    }
}
