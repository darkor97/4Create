using MediatR;

namespace Application.CQRS.Queries.Company
{
    public sealed record GetCompanyByIdQuery(Guid id) : IRequest<Domain.Entities.Company?>
    {
    }
}
