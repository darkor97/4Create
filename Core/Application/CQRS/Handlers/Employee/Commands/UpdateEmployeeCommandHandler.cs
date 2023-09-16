using Application.CQRS.Commands.Employee;
using Domain.Abstractions;
using MediatR;

namespace Application.CQRS.Handlers.Employee.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Domain.Entities.Employee>
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IRepository<Domain.Entities.Employee> employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Entities.Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateAsync(request.Employee);
            await _unitOfWork.SaveChangesAsync();

            return request.Employee;
        }
    }
}
