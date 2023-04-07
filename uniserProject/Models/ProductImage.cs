using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniserProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
