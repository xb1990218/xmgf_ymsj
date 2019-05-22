using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        #region 实体集
        public DbSet<UserInfo> UserInfo { get; set; } //注意 这里名字和实体名字必须一致
        public DbSet<User> User { get; set; }
        #endregion
    }
}
