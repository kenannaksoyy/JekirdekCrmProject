using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Dto.Request
{
    /// <summary>
    /// Müşterileri Filtrelemek İçin Özellileri Barındıran Ui Nesnesi
    /// </summary>
    public class CustomerFilterRequest
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public CustomerFilterRequest() {}
        /// <summary>
        /// Filtre Müşteri İsmi
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Filtre Müşteri Bölgesi
        /// </summary>
        public string? Region { get; set; }
        /// <summary>
        /// Filtre Tarih Başlangıçı
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// Filtre Tarih Sonu
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
