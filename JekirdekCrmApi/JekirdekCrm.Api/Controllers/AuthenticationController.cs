﻿using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Interface.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Kullanıcı Login Authentication Sağlamaktadır
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> UserLoginAsync([FromBody] UserLoginRequest userLoginRequest)
        {
            try
            {
                UserLoginResponse userLoginResponse = await _authenticationService.UserLoginAsync(userLoginRequest);
                return Ok(new
                {
                    IsError = false,
                    CustomerLogin = userLoginResponse
                });
            }
            catch (Exception ex)
            {
                //401 Kodu İle Hatayı Döndür
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    IsError = true,
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
