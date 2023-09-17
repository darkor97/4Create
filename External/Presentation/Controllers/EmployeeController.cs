using Application.CQRS.Commands.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Presentation.Controllers
{
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] EmployeeRequest employeeRequest)
        {
            var command = new CreateEmployeeCommand(employeeRequest.Title, employeeRequest.Email);

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }


            return StatusCode(Status200OK, "Employee created successfully");
        }
    }
}
