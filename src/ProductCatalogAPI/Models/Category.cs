using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The name must be at least 3 and at max 100 characters long.", MinimumLength = 3)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
    }
}