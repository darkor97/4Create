using Application.CQRS.Queries.Employee;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Employee.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Domain.Entities.Employee?>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;

        public GetEmployeeByIdQueryHandler(IRepository<Domain.Entities.Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository
                .GetAsync(request.Id);
        }
    }
}
