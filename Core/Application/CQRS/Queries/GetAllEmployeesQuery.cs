using Domain.Entities;
using MediatR;

namespace Application.CQRS.Queries
{
    public sealed record GetAllEmployeesQuery : IRequest<IEnumerable<Employee>>
    {
    }
}
