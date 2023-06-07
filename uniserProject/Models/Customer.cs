using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number.")]
        public string PhoneNumber { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
