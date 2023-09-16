using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompanyCommandHandler(IRepository<Domain.Entities.Company> companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyRepository.DeleteAsync(request.Company);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
