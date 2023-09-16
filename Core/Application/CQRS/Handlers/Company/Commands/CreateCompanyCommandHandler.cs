using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Domain.Entities.Company>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandHandler(IRepository<Domain.Entities.Company> companyRepository, IRepository<Domain.Entities.Employee> employeeRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var employeesToCreate = request.Employees.Where(y => y.Id != Guid.Empty);
            foreach (var employee in employeesToCreate)
            {
                await _employeeRepository.CreateAsync(new Domain.Entities.Employee() { Email = employee.Email, Title = employee.Title });
            }

            var company = new Domain.Entities.Company()
            {
                Name = request.Name,
                Employees = (IList<Domain.Entities.Employee>)request.Employees
            };

            await _companyRepository.CreateAsync(company);
            await _unitOfWork.SaveChangesAsync();

            return company;
        }
    }
}
