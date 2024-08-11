using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class TenantUsers
    {
        [Key]
        public int PkID { get; set; }
        public int? TenantID { get; set; }
        public string AspUserID { get; set; }
        public string AspRoleID { get; set; }
        public int? RoleID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
