using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
        public DbSet<tblModule> tblModules { get; set; }
        public DbSet<tblModuleAccessRight> tblModuleAccessRights { get; set; }
        public DbSet<tblRefreshToken> tblRefreshTokens { get; set; }
    }
}
