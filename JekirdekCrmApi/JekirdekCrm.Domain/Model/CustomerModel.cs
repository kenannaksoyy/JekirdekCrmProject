using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Model
{
    /// <summary>
    /// Müşteri Application Nesnesi
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public CustomerModel() { }
        /// <summary>
        /// Müşteri Idsi
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
        /// Müşteri Bölge Bilgisi
        /// </summary>
        public string Region { get; set; } = string.Empty;
        /// <summary>
        /// Müşteri Kayıt Tarihi 
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
