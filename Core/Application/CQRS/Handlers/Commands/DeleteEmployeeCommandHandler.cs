using Application.CQRS.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IRepository<Employee> employeeRepository, IUnitOfWork unitOfWork)
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
