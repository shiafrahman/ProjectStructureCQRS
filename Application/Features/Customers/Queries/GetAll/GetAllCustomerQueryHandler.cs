using Application.Common.Exceptions;
using Application.Features.Customers.Models;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
    {
        private readonly IRepository<Customer> _repository;

        public GetAllCustomerQueryHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetAllAsync();
            if (customer == null)
                throw new NotFoundException(nameof(Customer), request);
            return customer;
        }
    }
}
