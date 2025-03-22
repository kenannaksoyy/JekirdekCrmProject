using JekirdekCrm.Domain.Dto.Request;
using JekirdekCrm.Domain.Dto.Response;
using JekirdekCrm.Domain.Interface.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<UserLoginResponse> UserLoginAsync(UserLoginRequest userLoginRequest)
        {
            throw new NotImplementedException();
        }
    }
}
