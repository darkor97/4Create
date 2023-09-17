using Application.CQRS.Commands.Company;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;
namespace Presentation.Controllers
{
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CompanyController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyRequest companyRequest)
        {
            var command = new CreateCompanyCommand(companyRequest.Name, _mapper.Map<IEnumerable<Employee>>(companyRequest.Employees));

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            return StatusCode(Status200OK, "Company created successfully");
        }
    }
}
