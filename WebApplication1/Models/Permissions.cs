using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Permissions
    {
        [Key]
        public int PermissionID { get; set; }

        public int? RoleID { get; set; }

        public int? FormID { get; set; }

        public bool? CanView { get; set; }

        public bool? CanEdit { get; set; }    

        public bool? CanAdd { get; set; }

        public bool? CanDelete { get; set; }  

        public int? AccessPermissionID { get; set; }
    }
}
