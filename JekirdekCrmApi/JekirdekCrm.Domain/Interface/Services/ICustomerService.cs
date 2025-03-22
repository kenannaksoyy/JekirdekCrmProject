using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Interface.Services
{
    /// <summary>
    /// Kullanıcıların Rolleri ile Kullanacağı Müşteri Servisleri
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Tüm Müşterileri Getirir
        /// Tabloyu Doldurcak
        /// Admin ve User Kullanabilir
        /// Controllerdan Authorization Sağlanacak
        /// Servislerde Role Dair Birşey Olmayacak
        /// </summary>
        /// <returns></returns>
        public Task<List<CustomerResponse>> GetAllAsync();

        /// <summary>
        /// Idye göre müşteri getirme
        /// Admin ve User Kullanabilir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<CustomerResponse> GetByIdAsync(int id);

        /// <summary>
        /// Yeni Müşteri Oluşturma
        /// Sadece Admin Kullanacak
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        public Task<int> CreateAsync(CustomerRequest customerRequest);

        /// <summary>
        /// Mevcut Müşteriyi Güncelleme
        /// Sadece Admin Kullanacak
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        public Task UpdateAsync(CustomerRequest customerRequest);

        /// <summary>
        /// Mevcut Müşteriyi Id ile Silme
        /// Sadece Admin Kullanacak
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id);

    }
}
