
using Application.Features.Customers.Models;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands
{
    public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IRepository<Customer> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Name))
                throw new ArgumentException("Customer name cannot be empty.");

            var customer = _mapper.Map<Customer>(request);
            await _repository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return customer;
        }
    }
}
