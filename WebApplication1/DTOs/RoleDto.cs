using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class RoleDto
    {
    }

    public class CreateRoleDto
    {
        [Required]
        public string? RoleName { get; set; }
    }

    public class UpdateRoleDto
    {
        [Required]
        public int RoleID { get; set; }

        [Required]
        public string? RoleName { get; set; }
    }
}
