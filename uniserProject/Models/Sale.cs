using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookProject.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public string SaleCode { get; set; }
        public DateTime Date { get; set; }
        public int StoreId { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public Store Store { get; set; }
        public Book Book { get; set; }
        public Customer Customer
        {
            get; set;
        }
        
    }

}
