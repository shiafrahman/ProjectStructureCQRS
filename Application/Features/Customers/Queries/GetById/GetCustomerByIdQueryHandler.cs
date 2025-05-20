using Application.Common.Exceptions;
using Application.Features.Customers.Models;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries
{
    public class GetCustomerByIdQueryHandler: IRequestHandler<GetCustomerByIdQuery, Customer?>
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomerByIdQueryHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer == null)
                throw new NotFoundException(nameof(Customer), request.Id);
            return customer;
        }
    }
}
