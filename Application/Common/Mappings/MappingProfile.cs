using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Registration;
using Application.Features.Customers.Commands;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Models;
using AutoMapper;
using Core.Dtos.Auth;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();
            CreateMap<RegisterCommand, RegisterDto>();
            CreateMap<LoginCommand, LoginDto>();
        }
    }
}
