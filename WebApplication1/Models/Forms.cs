using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Forms
    {
        [Key]
        public int FormID { get; set; }

        public string FormName { get; set; }

        public string FormUrl { get; set; }
    }
}
