using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Entity
{
    [Table(nameof(Product))]
    public class Product : IBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}