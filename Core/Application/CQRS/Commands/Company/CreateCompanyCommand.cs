using MediatR;

namespace Application.CQRS.Commands.Company
{
    public sealed record CreateCompanyCommand(string Name, IEnumerable<Domain.Entities.Employee> Employees) : IRequest<Domain.Entities.Company>
    {
    }
}
