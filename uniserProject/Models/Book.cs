using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(5, ErrorMessage = "The field must contain exactly 5 characters.")]
        public string Code { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
