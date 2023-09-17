using Application.CQRS.Commands.Company;
using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Domain.Entities.Employee>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCompanyCommand> _logger;

        public UpdateEmployeeCommandHandler(IRepository<Domain.Entities.Employee> employeeRepository, IUnitOfWork unitOfWork, ILogger<UpdateCompanyCommand> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Domain.Entities.Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateAsync(request.Employee);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Update,
                ResourceType = ResourceType.Employee,
                Comment = "Employee updated",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return request.Employee;
        }
    }
}
