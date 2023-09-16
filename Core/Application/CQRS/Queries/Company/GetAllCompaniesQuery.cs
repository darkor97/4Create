using MediatR;

namespace Application.CQRS.Queries.Company
{
    public sealed record GetAllCompaniesQuery() : IRequest<IEnumerable<Domain.Entities.Company>>
    {
    }
}
