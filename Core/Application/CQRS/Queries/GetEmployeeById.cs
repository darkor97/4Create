using Domain.Entities;
using MediatR;

namespace Application.CQRS.Queries
{
    public sealed record GetEmployeeById(Guid Id) : IRequest<Employee>
    {
    }
}
