using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Entity
{
    /// <summary>
    /// Users Tablosu Nesnesi
    /// </summary>
    public class User
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public User() { }
        /// <summary>
        /// Kullanıcı Idsi
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Kullanıcı Adı
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Kullanıcı Şifresi Hashlencek
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Kullanıcı Sistem Rolü
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// Kullanıcının Oluşturulma Tarihi PostreSql ile Uygun Date Formatı
        /// Kodda Register İstenmedi
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
        /// <summary>
        /// Kullanıcının Güncellenme Tarihi PostreSql ile Uygun Date Formatı
        /// Kodda Kullanıcı Güncellemede İstenmedi
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;
    }
}
