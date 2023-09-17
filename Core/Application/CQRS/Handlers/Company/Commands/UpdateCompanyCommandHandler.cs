using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Domain.Entities.Company>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;

        public UpdateCompanyCommandHandler(
            IRepository<Domain.Entities.Company> companyRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyRepository.UpdateAsync(request.Company);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Update,
                ResourceType = ResourceType.Company,
                Comment = "Company update",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return request.Company;
        }
    }
}
