using EmployeeManagement.Application.Features;
using EmployeeManagement.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/v1/employee")]
    [OpenApiTag("Employee")]
    [Produces("application/json")]

    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(
            IMediator mediator,
            ApplicationDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpPost("list")]
        public async Task<IActionResult> ListAsync([FromBody] ListEmployeeQuery query)
        {
            var response = await _mediator.Send(query);

            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetEmployeeByIdQuery { id = id });

            return response != null ? Ok(response) : BadRequest(response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeCommand query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeeCommand query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> ViewAll()
        {
            var response = await _dbContext.Employees.ToListAsync();

            return response != null ? Ok(response) : BadRequest(response);
        }

    }
}
