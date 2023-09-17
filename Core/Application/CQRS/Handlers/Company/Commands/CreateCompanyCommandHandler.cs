using Application.CQRS.Commands.Company;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Company.Commands
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Domain.Entities.Company>
    {
        private readonly IRepository<Domain.Entities.Company> _companyRepository;
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;

        public CreateCompanyCommandHandler(
            IRepository<Domain.Entities.Company> companyRepository,
            IRepository<Domain.Entities.Employee> employeeRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreateCompanyCommandHandler> logger)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var employeesToCreate = request.Employees.Where(y => y.Id == Guid.Empty).ToList();
            var company = new Domain.Entities.Company()
            {
                Name = request.Name,
                Employees = employeesToCreate
            };

            foreach (var existingEmployee in request.Employees.Except(employeesToCreate!))
            {
                var employee = await _employeeRepository.GetAsync(existingEmployee.Id);
                company.Employees?.Add(employee!);
            }

            await _companyRepository.CreateAsync(company);
            await _unitOfWork.SaveChangesAsync();

            if (employeesToCreate?.Any() == true)
            {
                _logger.LogInformation("{@SystemLog}", new SystemLog()
                {
                    Event = Event.Create,
                    ResourceType = ResourceType.Employee,
                    Comment = "Employee create on company create",
                    CreatedAt = DateTime.UtcNow,
                    ChangeSet = new[] { company.Employees.Where(x => employeesToCreate.Any(y => y.Email == x.Email)).ToList() }
                });
            }
            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Create,
                ResourceType = ResourceType.Company,
                Comment = "Company create",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request.Name }
            });

            return company;
        }
    }
}
