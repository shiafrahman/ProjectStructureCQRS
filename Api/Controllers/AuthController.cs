using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Registration;
using Core.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var command = new RegisterCommand(registerDto.Email, registerDto.Password, registerDto.Role);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto.Email, loginDto.Password);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
