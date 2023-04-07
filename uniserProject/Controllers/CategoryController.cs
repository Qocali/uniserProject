using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uniserProject.DAL;
using uniserProject.Models;

namespace uniserProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> FilterforYear(string year)
        {

            List<Product> products = await _db.Products.Where(x => x.Year.Year.ToString() == year).Include(x => x.Images).ToListAsync();
            if (year == null)
            {
                List<Product> product1 = await _db.Products.OrderByDescending(x => x.Id).Include(x => x.Images).Take(10).ToListAsync();
                return PartialView("_FilterYearPartial", product1);
            }
            return PartialView("_FilterYearPartial", products);
        }
        public async Task<IActionResult> FilterforPrice(int maxprice,int minprace)
        {

            List<Product> products = await _db.Products.Where(x =>x.ProductDetails.Price>=minprace && x.ProductDetails.Price<=maxprice).Include(x => x.Images).ToListAsync();
            if (maxprice==0 && minprace!=0)
            {
                List<Product> products1 = await _db.Products.Where(x => x.ProductDetails.Price >= minprace).Include(x => x.Images).ToListAsync();
                return PartialView("_FilterPricePartial", products1);
            }
            if(maxprice!=0 && minprace == 0)
            {
                List<Product> products2 = await _db.Products.Where(x => x.ProductDetails.Price <= maxprice).OrderByDescending(x => x.Id).Include(x => x.Images).Take(10).ToListAsync();
                return PartialView("_FilterPricePartial", products2);
            }
            if(maxprice == 0 && minprace == 0)
            {
                List<Product> products3 = await _db.Products.OrderByDescending(x => x.Id).Include(x => x.Images).Take(10).ToListAsync();
                return PartialView("_FilterPricePartial", products3);
            }
            return PartialView("_FilterPricePartial", products);
        }
        public async Task<IActionResult> FilterforMarka(int markaid)
        {

            List<Product> products = await _db.Products.Where(x => x.MarkaId==markaid).Include(x => x.Images).ToListAsync();
            if (markaid == null)
            {
                List<Product> product1 = await _db.Products.OrderByDescending(x => x.Id).Include(x => x.Images).Take(3).ToListAsync();
                return PartialView("_FilterMarkaPartial", product1);
            }
            return PartialView("_FilterMarkaPartial", products);
        }

    }
}
