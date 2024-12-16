using Microsoft.AspNetCore.Mvc;
using shop_Web.Data;
using shop_Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace shop_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Index action (this will display the collections)
        public IActionResult Index(string query)
        {
            var collections = new Dictionary<string, List<Card>>
    {
        {
            "Shoes", new List<Card>
            {
                new Card { cat_id=1, Title = "Nike Shoes", Description = "Blue Sneakers & Slides", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRTlV3kg8eVsGm5VAICY7YQqWDAw1DzvaIgMKhUXDoebCPIKmYnw3og6Y-46JY2LNo15Wg&usqp=CAU" },
                new Card { cat_id=1, Title = "Adidas Shoes", Description = "Stylish Adidas Shoes", ImageUrl = "https://www.batabd.com/cdn/shop/files/1_b1fbf703-5310-43c2-b2e9-fef356b6036c_1024x1024.png?v=1712138150" },
                new Card { cat_id=1, Title = "Converse Shoes", Description = "Trendy Converse Shoes", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQF8yiY2ELRWR0jSFzT5bSE3ohnJU68yo1kCwNPDAXDzxSV-GnPWm1eXGPc9JlG2MCtq0Y&usqp=CAU" }
            }
        },
        {
            "Bags", new List<Card>
            {
                new Card { cat_id = 2, Title = "Leather Bag", Description = "Premium leather bag for office.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSg_IINW-45xinp67CgAiLicn1HOzV8PoUxIA&s" },
                new Card { cat_id = 2, Title = "Backpack", Description = "Durable backpack for travel.", ImageUrl = "https://cdn.thewirecutter.com/wp-content/media/2022/09/backpacks-2048px.jpg?auto=webp&quality=75&width=1024" },
                new Card { cat_id = 2, Title = "Tote Bag", Description = "Stylish tote bag for everyday use.", ImageUrl = "https://img.ltwebstatic.com/images3_pi/2021/12/14/16394734901f7531baf55486384e64c61576ad5191_thumbnail_405x552.jpg" }
            }
        },
        {
            "Laptops", new List<Card>
            {
                new Card { cat_id = 3, Title = "MacBook Pro", Description = "16-inch, M2 Chip, Silver.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHQj31Gyo8znRfKecYmyjqtLC7rcRk6754eA&s" },
                new Card { cat_id = 3, Title = "Dell XPS 13", Description = "13-inch, Intel Core i7, Ultra HD.", ImageUrl = "https://computermania-bd.b-cdn.net/wp-content/uploads/Dell-XPS-13-2-in-1-10.jpg" },
                new Card { cat_id = 3, Title = "HP Spectre x360", Description = "Convertible, 15-inch, Touchscreen.", ImageUrl = "https://computermania-bd.b-cdn.net/wp-content/uploads/HP-Spectre-x360-15-Price-in-BD.jpg" }
            }
        }
    };

            if (!string.IsNullOrEmpty(query))
            {
                // Filter the dictionary by checking if the category name contains the search query
                collections = collections
                    .Where(c => c.Key.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                c.Value.Any(i => i.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || i.Description.Contains(query, StringComparison.OrdinalIgnoreCase)))
                    .ToDictionary(c => c.Key, c => c.Value);
            }

            return View(collections); // Pass the filtered collections to the view
        }


        // Search action (this will return items from the database based on the search query)
        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index"); // If the query is empty, redirect to the Index action
            }

            // Fetch items from the database where the item name, description, or brand matches the query
            var results = _context.Item
                .Where(item => item.item_name.Contains(query) ||
                               item.item_des.Contains(query) ||
                               item.brand.Contains(query))
                .ToList();

            return View(results); // Pass the search results to the view
        }
    }
}
