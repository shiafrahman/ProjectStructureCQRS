using Application.Features.Customers.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Update
{
    public record UpdateCustomerCommand(int Id, string Name, string Email, string Phone) : IRequest<Customer>;
}
