using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class TenantDbContext : ApplicationDbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IConfiguration _configuration;

        public TenantDbContext(DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantId = _httpContextAccessor.HttpContext?.Items["TenantName"]?.ToString();
            if (tenantId != null)
            {
                var connectionString = _configuration.GetConnectionString("TenantConnection").Replace("{TenantName}", tenantId);
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                throw new Exception("Tenant ID is not set in HttpContext.");
            }
        }

        public DbSet<Products> Products { get; set; }

        public DbSet<ChatMessages> ChatMessages { get; set; }
    }
}
