using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IRepository<Domain.Entities.Employee> employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteAsync(request.Employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
