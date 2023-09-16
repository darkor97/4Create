using Application.CQRS.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public GetAllEmployeesQueryHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllAsync();
        }
    }
}
