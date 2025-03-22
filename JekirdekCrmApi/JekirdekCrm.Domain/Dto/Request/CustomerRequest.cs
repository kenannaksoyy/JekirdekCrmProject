using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Dto.Request
{
    /// <summary>
    /// Yeni Müşteri ve Güncellenecek Müşteri UI Nesnesi
    /// </summary>
    public class CustomerRequest
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public CustomerRequest() { }
        /// <summary>
        /// Müşteri Eşsiz Idsi
        /// Yeni Müşteride Id Olmayacak Güncellemede Id Olcak
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
    }
}
