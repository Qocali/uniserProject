using BookProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BookProject.Models;

namespace BookProject.DAL
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
       
    }
}
