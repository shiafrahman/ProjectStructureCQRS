﻿using Core.Dtos.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Registration
{
    public record RegisterCommand(string Email, string Password, string Role) : IRequest<AuthResponseDto>;
}
