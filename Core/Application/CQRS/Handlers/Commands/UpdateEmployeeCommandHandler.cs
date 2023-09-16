using Application.CQRS.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateAsync(request.Employee);
            await _unitOfWork.SaveChangesAsync();

            return request.Employee;
        }
    }
}
