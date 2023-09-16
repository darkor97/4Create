using Application.CQRS.Queries.Employee;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Employee.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Domain.Entities.Employee>>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;

        public GetAllEmployeesQueryHandler(IRepository<Domain.Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
