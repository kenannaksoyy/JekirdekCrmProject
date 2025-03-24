using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Dto.Request
{
    /// <summary>
    /// Kullanıcı Login Ekranı Login Talebi
    /// </summary>
    public class UserLoginRequest
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public UserLoginRequest() { }
        /// <summary>
        /// Giriş Yapmak İsteyen Kullanıcı Adı
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Giriş Yapmak İsteyen Kullanıc Şifresi (Hashlenmemiş)
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
