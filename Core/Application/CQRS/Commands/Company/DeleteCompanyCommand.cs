using MediatR;

namespace Application.CQRS.Commands.Company
{
    public sealed record DeleteCompanyCommand(Domain.Entities.Company Company) : IRequest
    {
    }
}
