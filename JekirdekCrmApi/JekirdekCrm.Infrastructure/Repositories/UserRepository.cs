using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> GetUserByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
