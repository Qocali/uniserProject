using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniserProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public DateTime Year { get; set; }
        public List<ProductImage> Images { get; set; }
        [NotMapped]
        public IFormFile[] Photo { get; set; }
        public ProductDetails ProductDetails { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Marka Marka { get; set; }
        public int MarkaId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
