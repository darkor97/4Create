using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Domain.Entities.Employee>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IRepository<Domain.Entities.Employee> employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<Domain.Entities.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee() { Email = request.Email, Title = request.Title };

            await _employeeRepository.CreateAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee;
        }
    }
}
