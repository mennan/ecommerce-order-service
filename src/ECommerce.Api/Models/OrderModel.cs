using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api
{
    public class OrderModel
    {
        [Required]
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}