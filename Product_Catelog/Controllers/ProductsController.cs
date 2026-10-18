using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Product_Catelog.Models;

namespace Product_Catelog.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products (Displays the product catalog listing)
        public ActionResult Index()
        {
            List<Product> catalog = GetProductCatalog();
            return View("Products", catalog);
        }

        // GET: Products/Details/{id} (Displays detailed specifications for a single product)
        public ActionResult Details(int id)
        {
            List<Product> catalog = GetProductCatalog();
            Product product = catalog.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound("Requested product could not be found.");
            }

            return View("View", product);
        }

        // Mock repository method providing sample catalog items
        private List<Product> GetProductCatalog()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId = 101,
                    ProductName = "Sony WH-1000XM5 Wireless ANC Headphones",
                    ProductPrice = 29990,
                    ProductCategory = "Audio",
                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800&auto=format&fit=crop&q=80",
                    Description = "Industry-leading active noise cancellation with integrated processor V1 and dual sensors for crystal-clear acoustics.",
                    Rating = 4.8,
                    InStock = true
                },
                new Product
                {
                    ProductId = 102,
                    ProductName = "Dell XPS 15 OLED Laptop",
                    ProductPrice = 174990,
                    ProductCategory = "Computers",
                    ImageUrl = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800&auto=format&fit=crop&q=80",
                    Description = "Stunning 3.5K OLED touchscreen display packed with 13th Gen Intel Core i7 and NVIDIA RTX 4060 graphics.",
                    Rating = 4.7,
                    InStock = true
                },
                new Product
                {
                    ProductId = 103,
                    ProductName = "Fujifilm X-T50 Mirrorless Camera",
                    ProductPrice = 129900,
                    ProductCategory = "Photography",
                    ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=800&auto=format&fit=crop&q=80",
                    Description = "40.2-megapixel X-Trans CMOS 5 HR sensor, dedicated film simulation dial, and built-in 7.0-stop IBIS.",
                    Rating = 4.9,
                    InStock = true
                },
                new Product
                {
                    ProductId = 104,
                    ProductName = "Apple Watch Ultra 2 Smartwatch",
                    ProductPrice = 89900,
                    ProductCategory = "Wearables",
                    ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800&auto=format&fit=crop&q=80",
                    Description = "Aerospace-grade titanium case with brightest Always-On Retina display and precision dual-frequency GPS.",
                    Rating = 4.9,
                    InStock = true
                },
                new Product
                {
                    ProductId = 105,
                    ProductName = "Logitech MX Master 3S Wireless Mouse",
                    ProductPrice = 9995,
                    ProductCategory = "Accessories",
                    ImageUrl = "https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=800&auto=format&fit=crop&q=80",
                    Description = "Quiet click switches with 8,000 DPI track-on-glass sensor and MagSpeed electromagnetic scroll wheel.",
                    Rating = 4.6,
                    InStock = true
                }
            };
        }
    }
}