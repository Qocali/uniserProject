using System.Collections.Generic;

namespace uniserProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<Marka> Marka { get; set; }
    }
}
