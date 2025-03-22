using AutoMapper;
using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
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
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public Task<int> CreateAsync(CustomerRequest customerRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            List<CustomerResponse> customerResponses = [];
            //Dbden Müşterileri Aldık
            List<Customer> customers = await _customerRepository.GetCustomersAsync();
            if(customers.Any())
            {
                //Db Nesnemizi App Nesnemize Çevirdik
                List<CustomerModel> customerModels = _mapper.Map<List<CustomerModel>>(customers);
                //App Nesnemizi Ui Nesnemize Çevirdik
                customerResponses = _mapper.Map<List<CustomerResponse>>(customerModels);
            }
            return customerResponses;
        }

        public Task<CustomerResponse> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CustomerRequest customerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
