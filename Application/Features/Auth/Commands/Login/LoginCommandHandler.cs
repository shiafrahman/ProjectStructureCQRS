using AutoMapper;
using Core.Dtos.Auth;
using Core.Interfaces.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = _mapper.Map<LoginDto>(request);
            return await _authService.LoginAsync(loginDto);
        }
    }
}
