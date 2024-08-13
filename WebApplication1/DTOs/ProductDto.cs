using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryID { get; set; }
        public int? SellerID { get; set; }
        //public string CategoryName { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        //[Required]
        public int? CategoryID { get; set; }
        //[Required]
        public int? SellerID { get; set; }
    }

    public class UpdateProductDto
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public int? CategoryID { get; set; }
        [Required]
        public int? SellerID { get; set; }
    }
}
