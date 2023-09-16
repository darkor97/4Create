using Application.CQRS.Commands.Company;
using AutoMapper;
using DnsClient.Internal;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
        private readonly ILogger<Company> _logger;

        public CompanyController(ISender sender, IMapper mapper, ILogger<Company> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
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

            _logger.LogError(new SystemLog()
            {
                Comment = "Test create",
                CreatedAt = DateTime.UtcNow,
                Event = Domain.Enums.Event.Create,
                ResourceType = Domain.Enums.ResourceType.Company,
                ChangeSet = new[] { companyRequest }
            }.ToJson());

            return StatusCode(Status200OK, "Company created successfully");
        }
    }
}
