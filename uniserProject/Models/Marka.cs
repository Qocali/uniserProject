using System.Collections.Generic;

namespace uniserProject.Models
{
    public class Marka
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
        public int CatId { get; set; }
    }
}
