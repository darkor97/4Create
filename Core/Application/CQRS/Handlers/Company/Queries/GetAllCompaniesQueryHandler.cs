using Application.CQRS.Queries.Company;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Company.Queries
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<Domain.Entities.Company>>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly ILogger<GetAllCompaniesQueryHandler> _logger;

        public GetAllCompaniesQueryHandler(IRepository<Domain.Entities.Company> companyRepository, ILogger<GetAllCompaniesQueryHandler> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Domain.Entities.Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync();
            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Read,
                ResourceType = ResourceType.Company,
                Comment = "Companies read",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return companies;
        }
    }
}
