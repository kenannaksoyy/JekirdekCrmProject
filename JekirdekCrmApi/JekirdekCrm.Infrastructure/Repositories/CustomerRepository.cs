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

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            //PostreSql Uygun Tarih Formatı
            customer.RegistrationDate = DateTime.Now.Date
                .ToUniversalTime();
            await _jekirdekCrmDbContext.AddAsync(customer);
            await _jekirdekCrmDbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<bool> CheckUniqueCustomerAsync(string email, int? id)
        {
            //Emaile Bak Uniquelik Emailde Olcak Aynı Müşteri Kontrolüde Sağla
            Customer? possibleCustomer = await _jekirdekCrmDbContext.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.Id != id);
            return possibleCustomer == null;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _jekirdekCrmDbContext.Remove(customer);
            await _jekirdekCrmDbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int? id)
        {
            Customer? customer = await _jekirdekCrmDbContext.Customers
                .FirstOrDefaultAsync(customer => customer.Id == id);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            List<Customer> customers = await  _jekirdekCrmDbContext.Customers
                .ToListAsync();
            return customers;
        }

        public async Task UpdateCustomerAsync(Customer editingCustomer, Customer existingCustomer)
        {
            
            //Belirli Alanları Setle
            existingCustomer.FirstName = editingCustomer.FirstName;
            existingCustomer.LastName = editingCustomer.LastName;
            existingCustomer.Email = editingCustomer.Email;
            existingCustomer.Region = editingCustomer.Region;

            await _jekirdekCrmDbContext.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetFilteredCustomersAsync(string? name, string? region, DateTime? startDate, DateTime? endDate)
        {
            var query = _jekirdekCrmDbContext.Customers.AsQueryable();
            //Müşteri İsmi Varsa Ekle
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FirstName.ToLower() == name.ToLower());
            }
            //Müşteri Bölgesi Varsa Ekle
            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(c => c.Region == region);
            }
            if (startDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate >= startDate.Value.ToUniversalTime());
            }

            if (endDate.HasValue)
            {
                query = query.Where(c => c.RegistrationDate <= endDate.Value.ToUniversalTime());
            }

            return await query.ToListAsync();
        }
    }
}
