using JekirdekCrm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Interface.Repositories
{
    /// <summary>
    /// Users Tablo Operasyonları
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Kullanıcı Adı Eşsizdir 
        /// (Id Kullanılmayacak Login Ekranı Senaryosu Sadece Sadece)
        /// Validasyon App Katmanında Olcak Null Dönebilir
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<User?> GetUserByUserNameAsync(string userName);
    }
}
