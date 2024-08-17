using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class vw_api_tenants_chat_users
    {
        [Key]
        public int PkID { get; set; }
        public Nullable<int> TenantID { get; set; }
        public string? AspUserID { get; set; }
        public string? UserName { get; set; }
        public string? TenantUserName { get; set; }
        public string? FullName { get; set; }

    }
}
