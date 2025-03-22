using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Domain.Interface.Services;
using JekirdekCrm.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository ;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public Task<int> CreateAsync(CustomerRequest customerRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerModel>> GetAllAsync()
        {
            //Deneme Amaçlı Test Edilcek
            List<Customer> customers = await _customerRepository.GetCustomersAsync();
            return new List<CustomerModel>();
        }

        public Task<CustomerModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerRequest customerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
