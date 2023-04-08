using Final.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            ViewBag.Year = await _db.Products.Select(x => x.Year).Distinct().ToListAsync(); ;
            ViewBag.Marka = await _db.Marka.ToListAsync();
            ViewBag.Catigories = await _db.Category.ToListAsync();
            ViewBag.Page = page;
            ViewBag.Pagecount = Math.Ceiling((decimal)_db.Products.Count() /10);
            List<Product> products = await _db.Products.Include(x=>x.Category).Include(x=>x.Images).OrderByDescending(x => x.Id).Skip((page - 1) * 10).Take(10).ToListAsync();


            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Marka = await _db.Marka.ToListAsync();
            ViewBag.Catigories = await _db.Category.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? CatId,Product product)
        {
            ViewBag.Marka= await _db.Marka.ToListAsync();
            ViewBag.Catigories = await _db.Category.ToListAsync();
            List<ProductImage> productImages = new List<ProductImage>();
            foreach (var Photo in product.Photo)
            {
                var productimage = new ProductImage();
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (product.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa sekil elave edin!");
                    return View();
                }
                if (!Photo.isImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa sekil elave et!");
                    return View();

                }
                if (Photo.isLower4mb())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa 4mb kecmeyin!");
                    return View();
                }


                string folder = Path.Combine(_env.WebRootPath, "img", "product");
                productimage.ImageName = await Photo.savefileAsync(folder);
                productimage.ProductId = product.Id;
                productimage.Product=product;
                productImages.Add(productimage);
            }

            bool exist2 = _db.Products.Any(x => x.Name == product.Name);
            if (exist2)
            {
                ModelState.AddModelError("Name", "Bu ad movcuddur");
                return View();
            }
            var productdetail = new ProductDetails();
            productdetail.Detail = product.ProductDetails.Detail;
            productdetail.ProductId = product.Id;
            productdetail.Price = product.ProductDetails.Price;
            productdetail.Count = product.ProductDetails.Count;
            productdetail.Product = product;
            await _db.Products.AddAsync(product);
            await _db.ProductDetails.AddAsync(productdetail);
            product.CategoryId = (int)CatId;
            product.Images = productImages;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProductSearch(string keyword)
        {

            List<Product> products = await _db.Products.Include(x => x.Images).Where(x => x.Name.Contains(keyword)).ToListAsync();
            if (keyword == null)
            {
                List<Product> product1 = await _db.Products.Include(x => x.Images).OrderByDescending(x => x.Id).Take(10).ToListAsync();
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
            Product product = await _db.Products.Include(x => x.ProductDetails).Include(x=>x.Images).FirstOrDefaultAsync(x => x.Id == id);

            return View(product);
        }
        public async Task<IActionResult> FilterforCategory(int? category)
        {
            ViewBag.cat=category;
            ViewBag.Year = await _db.Products.Select(x => x.Year).Distinct().ToListAsync(); ;
            var cat =  _db.Category.Include(x=>x.Marka).FirstOrDefault(x=>x.Id==category);
            ViewBag.Catigories = await _db.Category.ToListAsync();
            List<Product> products = await _db.Products.Where(x => x.CategoryId == category).Include(x => x.Images).ToListAsync();
            var Mark = new List<Marka>();
            foreach(var marka in cat.Marka){
               var mark= _db.Marka.FirstOrDefault(x => x.Id == marka.Id);
                Mark.Add(mark);
            }
            ViewBag.Marka = Mark;
            if (category == null)
            {
                List<Product> product1 = await _db.Products.OrderByDescending(x => x.Id).Include(x => x.Images).Take(10).ToListAsync();
                return PartialView("_CategoryPartial", product1);
            }
            return PartialView("_CategoryPartial", products);
        }


    }
   
}
