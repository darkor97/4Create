using Application.CQRS.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeById, Employee?>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public GetEmployeeByIdQueryHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee?> Handle(GetEmployeeById request, CancellationToken cancellationToken)
        {
            return await _employeeRepository
                .GetAsync(request.Id);
        }
    }
}
