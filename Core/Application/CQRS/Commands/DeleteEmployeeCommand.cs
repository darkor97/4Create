using Domain.Entities;
using MediatR;

namespace Application.CQRS.Commands
{
    public sealed record DeleteEmployeeCommand(Employee Employee) : IRequest
    {
    }
}
