using Application.CQRS.Queries.Employee;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Handlers.Employee.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Domain.Entities.Employee>>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly ILogger<GetAllEmployeesQueryHandler> _logger;

        public GetAllEmployeesQueryHandler(IRepository<Domain.Entities.Employee> employeeRepository, ILogger<GetAllEmployeesQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Domain.Entities.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();

            _logger.LogInformation("{@SystemLog}", new SystemLog()
            {
                Event = Event.Read,
                ResourceType = ResourceType.Employee,
                Comment = "Employees read",
                CreatedAt = DateTime.UtcNow,
                ChangeSet = new[] { request }
            });

            return employees;
        }
    }
}
