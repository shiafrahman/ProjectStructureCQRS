using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Application.Features.Customers.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customer = await _mediator.Send(new GetAllCustomerQuery());
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (customer == null) return NotFound();
            return Ok(customer);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
    }
}
