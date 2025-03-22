using AutoMapper;
using JekirdekCrm.CrossCutting.Exceptions;
using JekirdekCrm.CrossCutting.Helper;
using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Interface.Authentication;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            //appsetting.jsondan Bilgi Çekimine Olanak Sağlamaktadır
            _configuration = configuration;
        }

        public async Task<UserLoginResponse> UserLoginAsync(UserLoginRequest userLoginRequest)
        {
            //string Alanları Bir trimle
            StringHelper.TrimStringProperties(userLoginRequest);
            //Kullanıcı Validaysonu
            UserModel userModel = await LoginValidate(userLoginRequest);

            //Tokenimizin İçinde Bulunmasını İstediğimiz Özellikler 
            List<Claim> claims =
            [
                new (ClaimTypes.Name, userModel.UserName),
                    new (ClaimTypes.Role, userModel.Role)
            ];

            //Claimleri vererek tokenmiz oluşuyor
            string token = GenerateToken(claims);

            //Login response hazırlanıyor token ve username barındırmaktadır
            UserLoginResponse userLoginResponse = new()
            {
                UserName = userLoginRequest.UserName,
                Token = token
            };
            return userLoginResponse;
        }

        /// <summary>
        /// Kullanıcının Login İçin Requestin Gerekli Validasyonları Sağlanmaktadır
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        private async Task<UserModel> LoginValidate(UserLoginRequest userLoginRequest)
        {
            try
            {
                //Gelen Requestin Eksiklik Kontrolü
                if (string.IsNullOrEmpty(userLoginRequest.UserName) || string.IsNullOrEmpty(userLoginRequest.UserName))
                {
                    //Hazırda Vardı
                    throw new MissingFieldException("Kullanıcının Eksik Bilgileri Mevcut");
                }

                //Kullanıcı İsim Kontrolü
                User? user = await _userRepository.GetUserByUserNameAsync(userLoginRequest.UserName)
                    ?? throw new NotFoundException($"{userLoginRequest.UserName} ile Kayıtlı Bir Kullanıcı Bulunamadı");

                //Db Nesnemizi App Nesnesine Çevirdik
                UserModel userModel = _mapper.Map<UserModel>(user);

                //Kullanıcının Şifre Kontrolü Dbye Şifre Hashlenerek Eklenmiştir bu sebeple Decode Verify Yapılması Gerekir
                bool passwordCheck = PasswordHelper.VerifyPassword(userLoginRequest.Password, userModel.Password);
                if (!passwordCheck)
                {
                    throw new PasswordErrorException("Kullanıcının Şifresi Hatalıdır");
                }

                return userModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Jwt Tokenimizi Oluşturcak
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private string GenerateToken(List<Claim> claims)
        {
            //Token Keyimizi Bytelarına Ayırdık
            SymmetricSecurityKey AuthSignKey = new(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));

            //Token Ömrümüzü Belirttik appsetting.jsondan Belirlenen Kriterde
            long tokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);

            //Token Güvenlik Bilgileri Verildi
            //Issuer tokeni kimin verdiğini belirtir
            //Audience tokenin kimin için verildiğini belirtir
            //Expires tokenin ne zaman süresinin dolacağını belirtir - 3 saat
            //SigningCredentials tokenin imzalanması için kullanılacak kimlik bilgilerini belirtir secretkey ve güvenlik algoritması
            //Subject tokenin içereceği talepleri belirtir - username ve role
            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(AuthSignKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            //Token Belirlenen Özelikler İle Üretiliyor
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
