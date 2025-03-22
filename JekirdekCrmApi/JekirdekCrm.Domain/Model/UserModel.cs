using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Model
{
    /// <summary>
    /// Kullanıcı Application Nesnesi
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Default Ctor
        /// </summary>
        public UserModel() { }
        /// <summary>
        /// Kullanıcı İsmi
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Kullanıcı Şifresi Hashlenmişter Olur Haşlenmemişte
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Kullancı Rolü
        /// </summary>
        public string Role { get; set; } = string.Empty;
    }
}
