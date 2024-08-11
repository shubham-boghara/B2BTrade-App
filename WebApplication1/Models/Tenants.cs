using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Tenants
    {
        [Key]
        public int TenantID { get; set; }

        public string TenantName { get; set; }

        public string Subdomain { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
