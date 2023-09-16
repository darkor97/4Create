using Application.CQRS.Queries.Company;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Company.Queries
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Domain.Entities.Company?>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;

        public GetCompanyByIdQueryHandler(IRepository<Domain.Entities.Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Domain.Entities.Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAsync(request.id);
        }
    }
}
