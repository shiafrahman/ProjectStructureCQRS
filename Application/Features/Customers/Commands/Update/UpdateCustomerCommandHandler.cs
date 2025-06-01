using Application.Features.Customers.Models;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(
            IRepository<Customer> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            //ILogger<UpdateCustomerCommandHandler> logger
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            // _logger = logger;
        }


        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new ArgumentException("Customer name cannot be empty.");
            }
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
            }
            try
            {
                _mapper.Map(request, customer);
                _repository.UpdateAsync(customer);

                var rowsAffected = await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Failed to update customer in the database.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
