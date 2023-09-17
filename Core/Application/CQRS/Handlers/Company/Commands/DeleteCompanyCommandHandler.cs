using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyCommandHandler(
            IRepository<Domain.Entities.Company> companyRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyRepository.DeleteAsync(request.Company);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Delete,
                ResourceType = ResourceType.Company,
                Comment = "Company delete",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });
        }
    }
}
