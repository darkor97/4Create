using MediatR;

namespace Application.CQRS.Queries.Employee
{
    public sealed record GetAllEmployeesQuery : IRequest<IEnumerable<Domain.Entities.Employee>>
    {
    }
}
