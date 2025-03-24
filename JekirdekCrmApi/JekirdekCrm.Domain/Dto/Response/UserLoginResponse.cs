using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Dto.Response
{
    /// <summary>
    /// Başarılı Login Talebi Sonucu Nesnesi
    /// Role ve Username İçinde Barındıran Bir Token Bulundurcak
    /// Kullanıcı Adı Cevapta Olcak
    /// </summary>
    public class UserLoginResponse
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public UserLoginResponse() { }
        /// <summary>
        /// Kullanıcı Oturum Tokeni
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// Kullanıcı Adı
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
