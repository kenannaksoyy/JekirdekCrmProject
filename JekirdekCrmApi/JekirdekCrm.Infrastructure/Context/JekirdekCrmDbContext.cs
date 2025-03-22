using JekirdekCrm.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCrm.Infrastructure.Context
{
    /// <summary>
    /// PostreSql Dbmizin Context Tablolarımız İçin Erişimiz Aracımız
    /// </summary>
    public class JekirdekCrmDbContext : DbContext
    {
        public JekirdekCrmDbContext(DbContextOptions<JekirdekCrmDbContext> options)
            : base(options)
        {
        }

        // Db Ve Entitylerimizin Eşlenmesi Tablo ve Key İsimleri Aynı Olmalıdır
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
