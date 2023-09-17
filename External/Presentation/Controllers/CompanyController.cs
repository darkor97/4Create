using Application.CQRS.Commands.Company;
using Application.CQRS.Queries.Company;
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
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllAsync()
        {
            var query = new GetAllCompaniesQuery();
            IEnumerable<Company> companies;

            try
            {
                companies = await _sender.Send(query);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            if (companies?.Any() != true)
            {
                return StatusCode(Status404NotFound, "No companies registered");
            }

            return StatusCode(Status200OK, companies);
        }

        [HttpGet]
        [Route("get/{id:guid}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetCompanyByIdQuery(id);
            Company? company;

            try
            {
                company = await _sender.Send(query);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            if (company == null)
            {
                return StatusCode(Status404NotFound, $"No company for passed id: {id}");
            }

            return StatusCode(Status200OK, company);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Company company)
        {
            var command = new UpdateCompanyCommand(company);

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            return StatusCode(Status200OK, company);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] Company company)
        {
            var command = new DeleteCompanyCommand(company);

            try
            {
                await _sender.Send(command);
            }
            catch (Exception ex)
            {
                return StatusCode(Status500InternalServerError, ex.GetApiExceptionMessage());
            }

            return StatusCode(Status200OK, "Company deleted");
        }
    }
}
