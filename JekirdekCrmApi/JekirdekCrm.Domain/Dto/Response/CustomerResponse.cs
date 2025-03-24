using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Dto.Response
{
    /// <summary>
    /// Crm Ekranında Müşteri Alanlarını Besleyecek
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public CustomerResponse() { }
        /// <summary>
        /// Müşteri Eşsiz Idsi
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Müşteri Adı
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Müşteri Soyadı
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Müşteri Emaili
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Müşteri Yaşadığı Bölge
        /// </summary>
        public string Region { get; set; } = string.Empty;
        /// <summary>
        /// Müşterinin Kayıt Tarihi 
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
