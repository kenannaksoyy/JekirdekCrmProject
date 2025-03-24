using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Domain.Interface.Authentication
{
    /// <summary>
    /// Kullanıcı Kimlik Doğrulama Servisleri
    /// Ama Şuan Sadece Login Olcak
    /// Ayrı Bir User Service Olmayacak User Sadece Login Olcak (Authorization CustomerServisleri Olcak)
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Kullanıcının Ad ve Şifre Bilgilerini Alarak Gerekli Kontrolleri Yapacak
        /// Kontrollerden Geçen Kullanıcıya Token ve RoleId Döncek 
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public Task<UserLoginResponse> UserLoginAsync(UserLoginRequest userLoginRequest);

    }
}
