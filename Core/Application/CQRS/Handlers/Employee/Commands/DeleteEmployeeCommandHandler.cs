using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandler(IRepository<Domain.Entities.Employee> employeeRepository, IUnitOfWork unitOfWork, ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteAsync(request.Employee);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Delete,
                ResourceType = ResourceType.Employee,
                Comment = "Employee deleted",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });
        }
    }
}
