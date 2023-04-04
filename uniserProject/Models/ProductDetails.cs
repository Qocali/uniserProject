using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace uniserProject.Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string   Price { get; set; }
        public string  Count { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public bool IsDeactive { get; set; }
    }
}
