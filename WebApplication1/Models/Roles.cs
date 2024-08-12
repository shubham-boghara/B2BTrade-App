using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        public int? TenantID { get; set; }

        public string? RoleName { get; set; }
    }
}
