using Application.CQRS.Queries.Employee;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Employee.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Domain.Entities.Employee?>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;

        public GetEmployeeByIdQueryHandler(IRepository<Domain.Entities.Employee> employeeRepository, ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Domain.Entities.Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository
                .GetAsync(request.Id);

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Read,
                ResourceType = ResourceType.Employee,
                Comment = "Employee read",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return employee;
        }
    }
}
