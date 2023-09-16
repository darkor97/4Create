using Application.CQRS.Queries.Company;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Company.Queries
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Domain.Entities.Company>>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;

        public GetAllCompaniesQueryHandler(IRepository<Domain.Entities.Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _companyRepository.GetAllAsync();
        }
    }
}
