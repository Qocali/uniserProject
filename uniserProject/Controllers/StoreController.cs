using BookProject.DAL;
using BookProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext _dbContext;

        public StoreController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Book
        public async Task<ActionResult> Index()
        {
            var Stores =await _dbContext.Stores.ToListAsync();
            return View(Stores);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Stores.Add(store);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var story = _dbContext.Stores.Find(id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(Store store)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(store).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var store = _dbContext.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("DeleteConfirm")]
        public ActionResult DeleteConfirm(int id)
        {
            var store = _dbContext.Stores.Find(id);
            _dbContext.Stores.Remove(store);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
