using Application.CQRS.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee> _employeeRepository;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IRepository<Employee> employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee() { Email = request.Email, Title = request.Title };

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee;
        }
    }
}
