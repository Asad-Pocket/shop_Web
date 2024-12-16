using Microsoft.AspNetCore.Mvc;
using shop_Web.Data;
using shop_Web.Models;

namespace shop_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext context;
        public CategoryController(ApplicationDBContext _context)
        {
            context = _context;
        }
        
        public IActionResult Index()
        {
            var categories = context.category.ToList();
            return View(categories);
        }
        [HttpPost]
        public IActionResult Create(string _cat)
        {
            if (string.IsNullOrWhiteSpace(_cat))
            {
                return BadRequest("Category name cannot be empty.");
            }
            var obj = new Category();
            obj.cat_name = _cat;

            context.category.Add(obj);
            context.SaveChanges();
            return Ok(true);
        }
        public IActionResult Edit(int cat_id)
        {
            if (cat_id != 0)
            {
                Category cat = context.category.Find(cat_id);
                return View(cat);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                context.category.Update(category);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int cat_id)
        {
            Category cat = context.category.Find(cat_id);
            context.category.Remove(cat);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
