using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class vw_api_tenants_my_roles
    {
        [Key]
        public int RoleID {get; set;}
        public string? RoleName {  get; set; }
        public int?   TenantID { get; set; }
    }
}
