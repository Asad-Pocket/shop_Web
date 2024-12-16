using Microsoft.AspNetCore.Mvc;
using shop_Web.Data;
using shop_Web.Models;
using System.Linq;

namespace shop_Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ItemController(ApplicationDBContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            // Fetch all items and map them to ItemDto
            var items = _context.Item
                .Join(
                    _context.category,
                    item => item.cat_id,
                    category => category.cat_id,
                    (item, category) => new ItemDto
                    {
                        item_id = item.item_id,
                        item_name = item.item_name,
                        item_des = item.item_des,
                        brand = item.brand,
                        unit_price = item.unit_price,
                        qty = item.qty,
                        cat_id = item.cat_id,
                        cat_name = category.cat_name
                    }
                ).ToList();

            return View(items);
        }


        public IActionResult Create()
        {
            return View();
        }
        // Add a new item
        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                // Add the new item to the database
                _context.Item.Add(item);
                _context.SaveChanges();
                return Ok("Item added successfully");
            }

            return BadRequest("Invalid data");
        }

        // Delete an item
        [HttpPost]
        public IActionResult Delete(int item_id)
        {
            // Find the item by ID
            var item = _context.Item.FirstOrDefault(i => i.item_id == item_id);
            if (item == null)
            {
                return NotFound("Item not found");
            }

            // Remove the item from the database
            _context.Item.Remove(item);
            _context.SaveChanges();
            return Ok("Item deleted successfully");
        }


        public IActionResult Edit(int item_id)
        {
            if (item_id != 0)
            {
                Item item = _context.Item.Find(item_id);

                return View(item);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetItemForEdit(int id)
        {
            var item = _context.Item.FirstOrDefault(i => i.item_id == id);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            return Ok(item);
        }

        // Edit an item (optional for now)
        [HttpPost]
        public IActionResult Edit(Item item)
        {

            if (item != null)
            {
                _context.Item.Update(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public IActionResult GetCategories()
        {
            var categories = _context.category.ToList();
            return Json(categories);
        }
    }
}
