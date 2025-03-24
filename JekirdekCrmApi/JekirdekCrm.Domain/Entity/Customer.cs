using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Entity
{
    /// <summary>
    /// Customers Tablosu Nesnesi
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public Customer() { }
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
        /// Müşterinin Emaili
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Müşterinin Yaşadığı Bölge 
        /// </summary>
        public string Region { get; set; } = string.Empty;
        /// <summary>
        /// Müşterinin Kayıt Tarihi PostreSql ile Uygun Date Formatı
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.Now.Date.ToUniversalTime();

    }
}
