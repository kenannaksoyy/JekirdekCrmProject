using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly JekirdekCrmDbContext _jekirdekCrmDbContext;
        public CustomerRepository(JekirdekCrmDbContext jekirdekCrmDbContext)
        {
            _jekirdekCrmDbContext = jekirdekCrmDbContext;
        }

        public Task<int> AddCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUniqueCustomerAsync(string email, int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = await  _jekirdekCrmDbContext.Customers
                .ToListAsync();
            return customers;
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
