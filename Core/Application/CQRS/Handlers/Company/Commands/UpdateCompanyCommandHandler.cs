using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Domain.Entities.Company>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyCommandHandler(IRepository<Domain.Entities.Company> companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            await _companyRepository.UpdateAsync(request.Company);
            await _unitOfWork.SaveChangesAsync();

            return request.Company;
        }
    }
}
