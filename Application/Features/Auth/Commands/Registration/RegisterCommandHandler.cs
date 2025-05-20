using AutoMapper;
using Core.Dtos.Auth;
using Core.Interfaces.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Registration
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = _mapper.Map<RegisterDto>(request);
            return await _authService.RegisterAsync(registerDto);
        }
    }
}
