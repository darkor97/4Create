using Application.CQRS.Queries.Company;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Company.Queries
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Domain.Entities.Company?>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly ILogger<GetCompanyByIdQueryHandler> _logger;

        public GetCompanyByIdQueryHandler(
            IRepository<Domain.Entities.Company> companyRepository,
            ILogger<GetCompanyByIdQueryHandler> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<Domain.Entities.Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.id);

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Read,
                ResourceType = ResourceType.Company,
                Comment = "Company read",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return company;
        }
    }
}
