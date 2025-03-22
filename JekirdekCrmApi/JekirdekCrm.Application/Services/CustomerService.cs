using AutoMapper;
using JekirdekCrm.CrossCutting.Exceptions;
using JekirdekCrm.CrossCutting.Helper;
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
        public async Task<int> CreateAsync(CustomerRequest customerRequest)
        {
            try
            {
                //Id Sayı değeri Gelebilir Ondan
                customerRequest.Id = null;
                StringHelper.TrimStringProperties(customerRequest);
                //Eksik Alan Varmı
                CheckMissedField(customerRequest);
                //Yeni Müşteri Talebi Uniquemi Kontrolü
                bool isUnique = await _customerRepository.CheckUniqueCustomerAsync(customerRequest.Email, customerRequest.Id);
                if (!isUnique)
                {
                    throw new ConflictException("Mail Adresi İle Kayıtlı Müşteri Bulunmaktadır");
                }

                //Validasyonlardan Gçeerse Db Nesnesine Dönüşümü Ve Eklenmesi
                CustomerModel customerModel = _mapper.Map<CustomerModel>(customerRequest);
                Customer customer = _mapper.Map<Customer>(customerModel);
                int newCustomerId =  await _customerRepository.AddCustomerAsync(customer);
                return newCustomerId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NotFoundException("Müşteri Silinmesinde Id Değeri 1'den Küçük Olamaz");
                }
                Customer? customer = await _customerRepository.GetCustomerByIdAsync(id) 
                    ?? throw new NotFoundException($"Silinmek İstenen {id}'li Müşteri Bulunamadı");

                //Validasyonlar Sonrası Müşteri Siliniyor
                await _customerRepository.DeleteCustomerAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            try
            {
                List<CustomerResponse> customerResponses = [];
                //Dbden Müşterileri Aldık
                List<Customer> customers = await _customerRepository.GetCustomersAsync();
                if (customers.Count != 0)
                {
                    //Db Nesnemizi App Nesnemize Çevirdik
                    List<CustomerModel> customerModels = _mapper.Map<List<CustomerModel>>(customers);
                    //App Nesnemizi Ui Nesnemize Çevirdik
                    customerResponses = _mapper.Map<List<CustomerResponse>>(customerModels);
                }
                return customerResponses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NotFoundException("Müşteri Id Değeri 1'den Küçük Olamaz");
                }

                Customer? customer = await _customerRepository.GetCustomerByIdAsync(id)
                ?? throw new NotFoundException($"{id} İd' li Müşteri Bulunamadı");

                //Db Nesnesinden Ui Nesnemize Çeviriyoruz
                CustomerModel customerModel = _mapper.Map<CustomerModel>(customer);
                CustomerResponse customerResponse = _mapper.Map<CustomerResponse>(customerModel);
                return customerResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(CustomerRequest customerRequest)
        {
            try
            {
                StringHelper.TrimStringProperties(customerRequest);
                if(customerRequest.Id == null ||customerRequest.Id <= 0)
                {
                    throw new NotFoundException("Müşteri Güncellenmesinde Id Değeri Valid");
                }
                CheckMissedField(customerRequest);

                //Müşteri Id Kontrolü Update İçin Existing Hazırlanması
                Customer existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerRequest.Id)
                    ?? throw new NotFoundException($"Güncellenecek Müşteri İçin {customerRequest.Id} li Müşteri Bulunamadı");

                //Unique Kontrolü
                bool isUnique = await _customerRepository.CheckUniqueCustomerAsync(customerRequest.Email, customerRequest.Id);
                if (!isUnique)
                {
                    throw new ConflictException("Güncellenmek İstenen Müşterinin Emaili Başka Bir Müşteriye Ait");
                }
                
                //Validasyonlar Sonrası Db Nesnesine Dönüşümü
                CustomerModel customerModel = _mapper.Map<CustomerModel>(customerRequest);
                Customer editedCustomer = _mapper.Map<Customer>(customerModel);
                await _customerRepository.UpdateCustomerAsync(editedCustomer, existingCustomer);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static void CheckMissedField(CustomerRequest customerRequest)
        {
            if(string.IsNullOrEmpty(customerRequest.FirstName) || 
                string.IsNullOrEmpty(customerRequest.LastName) || 
                string.IsNullOrEmpty(customerRequest.Email) ||
                string.IsNullOrEmpty(customerRequest.Region))
            {
                throw new MissingFieldException("Güncellenmek İstnen Müşterinin Eksik Alanı Mevcut");
            }
        }
    }
}
