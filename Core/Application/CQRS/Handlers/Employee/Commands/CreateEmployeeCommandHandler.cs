using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Domain.Entities.Employee>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Employee> employeeRepository, ILogger<CreateEmployeeCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Domain.Entities.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee() { Email = request.Email, Title = request.Title };

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Create,
                ResourceType = ResourceType.Employee,
                Comment = "New employee created",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return employee;
        }
    }
}
