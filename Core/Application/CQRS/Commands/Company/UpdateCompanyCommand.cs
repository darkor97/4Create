using MediatR;

namespace Application.CQRS.Commands.Company
{
    public sealed record UpdateCompanyCommand(Domain.Entities.Company Company) : IRequest<Domain.Entities.Company>
    {
    }
}
