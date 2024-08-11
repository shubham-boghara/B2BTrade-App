using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class DataAccessPermissions
    {
        [Key]
        public int AccessPermissionID { get; set; }

        public string AccessType { get; set; }
    }
}
