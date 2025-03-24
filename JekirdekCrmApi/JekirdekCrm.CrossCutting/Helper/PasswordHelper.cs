using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.CrossCutting.Helper
{
    /// <summary>
    /// Şifre Hashleme ve Doğrulama
    /// Dbye Kayıt Yaparken Şifre Hashlenmiş Olarak Kaydedilcek
    /// Dbden Çekilen Hash Password ile Loginden Gelen Çıplak Şifre Doğrulaması Yapılcak
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// Gelen Şifre Bilgisini Hashlemektedir
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string PasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        /// <summary>
        /// Hashlenmiş Şifre ile Gelen Çıplak Şifre Bilgisini Doğrulaması 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
