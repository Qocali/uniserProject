using Final.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using uniserProject.DAL;
using uniserProject.Models;

namespace uniserProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private int id;

        public HomeController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Catigories = await _db.Categories.ToListAsync();
            ViewBag.Page = page;
            ViewBag.Pagecount = Math.Ceiling((decimal)_db.Products.Count() / 3);
            List<Product> products = await _db.Products.Include(x=>x.Category).OrderByDescending(x => x.Id).Skip((page - 1) * 3).Take(3).ToListAsync();


            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Catigories = await _db.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? CatId,Product product)
        {
            ViewBag.Catigories = await _db.Categories.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (product.Photo == null)
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa sekil elave edin!");
                return View();
            }
            if (!product.Photo.isImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa sekil elave et!");
                return View();

            }
            if (product.Photo.isLower4mb())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa 4mb kecmeyin!");
                return View();
            }


            string folder = Path.Combine(_env.WebRootPath, "img", "product");
            product.Image = await product.Photo.savefileAsync(folder);


            bool exist2 = _db.Products.Any(x => x.Name == product.Name);
            if (exist2)
            {
                ModelState.AddModelError("Name", "Bu ad movcuddur");
                return View();
            }
            var productdetail = new ProductDetails();
            productdetail.ProductId = product.Id;
            productdetail.Price = product.ProductDetails.Price;
            productdetail.Count = product.ProductDetails.Count;
            productdetail.Product = product;
            await _db.Products.AddAsync(product);
            await _db.ProductDetails.AddAsync(productdetail);
            product.CategoryId = (int)CatId;
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProductSearch(string keyword)
        {

            List<Product> products = await _db.Products.Where(x => x.Name.Contains(keyword)).ToListAsync();
            if (keyword == null)
            {
                List<Product> product1 = await _db.Products.OrderByDescending(x => x.Id).Take(3).ToListAsync();
                return PartialView("_SearchProductPartial", product1);
            }
            return PartialView("_SearchProductPartial", products);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _db.Products.Include(x => x.ProductDetails).FirstOrDefaultAsync(x => x.Id == id);

            return View(product);
        }

    }
   
}
