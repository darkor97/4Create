using Application.CQRS.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Presentation.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Presentation.Controllers
{
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ISender sender, ILogger<EmployeeController> logger)
        {
            _sender = sender;
            _logger = logger;
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
                return StatusCode(Status500InternalServerError, ex.Message);
            }

            _logger.LogError(new SystemLog()
            {
                Comment = "Test create",
                CreatedAt = DateTime.UtcNow,
                Event = Domain.Enums.Event.Create,
                ResourceType = Domain.Enums.ResourceType.Employee,
                ChangeSet = new[] { employeeRequest }
            }.ToJson());

            return StatusCode(Status200OK, "Employee created successfully");
        }
    }
}
