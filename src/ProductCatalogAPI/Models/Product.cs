using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(100, ErrorMessage = "The name must be at least 3 and at max 100 characters long.", MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "The description must be at least 3 and at max 100 characters long.", MinimumLength = 3)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The price must be greater than or equal to zero.")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Invalid url format")]
        public string? ImageUrl { get; set; }
    }
}
