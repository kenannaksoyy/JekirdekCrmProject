using JekirdekCrm.Domain.Entity;
using JekirdekCrm.Domain.Interface.Repositories;
using JekirdekCrm.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JekirdekCrmDbContext _jekirdekCrmDbContext;
        public UserRepository(JekirdekCrmDbContext jekirdekCrmDbContext)
        {
            _jekirdekCrmDbContext = jekirdekCrmDbContext;
        }

        /// <summary>
        /// Kullanıcı Adı İle Kullanıcı Getirmektedir
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            User? user = await _jekirdekCrmDbContext.Users.FirstOrDefaultAsync(user => user.UserName == userName);
            return user;
        }
    }
}
