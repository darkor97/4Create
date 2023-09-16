using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands
{
    public sealed record UpdateEmployeeCommand(Employee Employee) : IRequest<Employee>
    {
    }
}
