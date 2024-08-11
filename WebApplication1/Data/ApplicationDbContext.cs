using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenants> Tenants { get; set; }

        public DbSet<TenantUsers> TenantUsers { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<DataAccessPermissions> DataAccessPermissions { get; set; }

        public DbSet<Forms> Forms { get; set; }

        public DbSet<Vw_Permissions> Vw_Permissions { get; set; }

    }
}
