using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ChatMessages
    {
        [Key]
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
