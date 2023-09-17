using Application.CQRS.Commands.Employee;
using Application.CQRS.Queries.Employee;
using Domain.Entities;
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


            return StatusCode(Status201Created, "Employee created successfully");
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllEmployeesQuery();
            IEnumerable<Employee> employees;

            try
            {
                employees = await _sender.Send(query);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            if (employees?.Any() != true)
            {
                return StatusCode(Status404NotFound, "No employees registered");
            }

            return StatusCode(Status200OK, employees);
        }

        [HttpGet]
        [Route("get/{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetEmployeeByIdQuery(id);
            Employee? employee;

            try
            {
                employee = await _sender.Send(query);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            if (employee == null)
            {
                return StatusCode(Status404NotFound, $"No employee for passed id: {id}");
            }

            return StatusCode(Status200OK, employee);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Employee employee)
        {
            var command = new UpdateEmployeeCommand(employee);

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            return StatusCode(Status200OK, employee);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] Employee employee)
        {
            var command = new DeleteEmployeeCommand(employee);

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            return StatusCode(Status200OK, "Employee deleted");
        }
    }
}
