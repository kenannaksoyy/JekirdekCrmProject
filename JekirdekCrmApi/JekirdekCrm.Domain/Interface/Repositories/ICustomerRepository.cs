using JekirdekCrm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Interface.Repositories
{
    /// <summary>
    /// Customers Tablo Operasyonları
    /// Command ve Queryler Bulunmaktadır (Az İşlem Var)
    /// First App Yaklaşımı
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Tüm Müşterileri Getirmektedir
        /// </summary>
        /// <returns></returns>
        public Task<List<Customer>> GetCustomersAsync();
        /// <summary>
        /// Id ile Müşteri Getirmektedir
        /// Validasyon App Katmanında Olcak Null Dönebilir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Customer?> GetCustomerByIdAsync(int? id);
        /// <summary>
        /// Müşteri Ekle
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Task<int> AddCustomerAsync(Customer customer);
        /// <summary>
        /// Müşteri Güncelle
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Task UpdateCustomerAsync(Customer editingCustomer, Customer existingCustomer);
        /// <summary>
        /// Müşteri Sil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteCustomerAsync(Customer customer);

        /// <summary>
        /// Müşteri Eşsizlik Kontrolü
        /// Hem Ekleme Hem Güncelleme Senaryoasunda kullanılacak
        /// Email Tek eşsizlik Kriteri
        /// Id İle Aynı Müşteri Engeli Koy (Güncellemede)
        /// Basit SP First App Hali Olmalı
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> CheckUniqueCustomerAsync(string email, int? id);

    }
}
