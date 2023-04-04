using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using uniserProject.Models;

namespace uniserProject.DAL
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
    }
}
