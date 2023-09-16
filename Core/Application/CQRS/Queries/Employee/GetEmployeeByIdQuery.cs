using MediatR;

namespace Application.CQRS.Queries.Employee
{
    public sealed record GetEmployeeByIdQuery(Guid Id) : IRequest<Domain.Entities.Employee?>
    {
    }
}
