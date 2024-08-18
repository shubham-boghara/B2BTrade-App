using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class TenantDbContext : ApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TenantDbContext(DbContextOptions<ApplicationDbContext> options,
                               IHttpContextAccessor httpContextAccessor,
                               IConfiguration configuration)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantId = _httpContextAccessor.HttpContext?.User.FindFirst("TenantName")?.Value;

            if (string.IsNullOrWhiteSpace(tenantId))
            {
                throw new InvalidOperationException("Tenant ID is not set in HttpContext.");
            }

            var connectionString = _configuration.GetConnectionString("TenantConnection")
                                                  .Replace("{TenantName}", tenantId);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string is not configured correctly.");
            }

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }
    }
}
