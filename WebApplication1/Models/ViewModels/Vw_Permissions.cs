using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.ViewModels
{
    [Table("Vw_Permissions")]
    public class Vw_Permissions
    {
        [Key]
        public int PermissionID { get; set; }
        public int? RoleID  { get; set; }
        public int? FormID  { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanAdd  { get; set; }
        public bool? CanDelete { get; set; }
        public int? AccessPermissionID { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public string AccessType { get; set; }
        public string FormUrl { get; set; }
    }
}
