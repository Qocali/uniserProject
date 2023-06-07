using BookProject.DAL;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookProject.Controllers
{
    public class SaleController : Controller
    {
        private readonly AppDbContext _db;
        public SaleController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Sale
        public async Task<ActionResult> Index()
        {
            var sales =await _db.Sales.Include(s => s.Store).Include(s => s.Book).Include(s => s.Customer).ToListAsync();
            return View(sales);
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            ViewBag.StoreId = new SelectList(_db.Stores, "Id", "Name");
            ViewBag.BookId = new SelectList(_db.Books, "Id", "Name");
            ViewBag.CustomerId = new SelectList(_db.Customers, "Id", "Name");
           
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Sale sale,string Status)
        {
           
            if (ModelState.IsValid)
            {
                var book=_db.Books.FirstOrDefault(b=>b.Id == sale.BookId);  
                var customer=_db.Customers.FirstOrDefault(b=>b.Id == sale.CustomerId);  
                var store=_db.Stores.FirstOrDefault(b=>b.Id == sale.StoreId); 
                sale.Book = book;
                sale.Customer = customer;
                sale.Store = store;
                sale.Status=Status;
                _db.Sales.Add(sale);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(sale);
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Sale sale = _db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewBag.StoreId = new SelectList(_db.Stores, "Id", "Name", sale.StoreId);
            ViewBag.BookId = new SelectList(_db.Books, "Id", "Name", sale.BookId);
            ViewBag.CustomerId = new SelectList(_db.Customers, "Id", "Name", sale.CustomerId);
            return View(sale);
        }

        // POST: Sale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sale sale)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(sale).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(sale);
        }
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Sale sale = _db.Sales.Find(id);
                if (sale == null)
                {
                    return NotFound();
                }
                return View(sale);
            }

            // POST: Sale/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Sale sale = _db.Sales.Find(id);
                _db.Sales.Remove(sale);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

    protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
