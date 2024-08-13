using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class GetUserProfileDto
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
    }

    public class CreateUserProfileDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? MiddleName { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
