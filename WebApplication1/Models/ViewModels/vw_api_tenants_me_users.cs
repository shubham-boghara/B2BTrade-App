using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class vw_api_tenants_me_users
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AspRoleName { get; set; }
        public string? RoleName { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        [Key]
        public int PkID { get; set; }

        public int? TenantID { get; set; }
    }
}
