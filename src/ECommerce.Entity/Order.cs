using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Entity
{
    [Table(nameof(Order))]
    public class Order : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float ItemPrice { get; set; }
        public float OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual Product Product { get; set; }
    }
}