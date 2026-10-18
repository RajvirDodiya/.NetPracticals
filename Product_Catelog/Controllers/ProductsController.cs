using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Product_Catelog.Models;

namespace Product_Catelog.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> products = GetProducts();
            return View("Products", products);
        }

        // GET: Products/Details/101
        public ActionResult Details(int id)
        {
            List<Product> products = GetProducts();
            Product product = products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View("View", product);
        }

        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { ProductId = 101, ProductName = "Sony WH-1000XM5", ProductPrice = 29990, ProductCategory = "Audio", ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500" },
                new Product { ProductId = 102, ProductName = "Dell XPS 15 OLED", ProductPrice = 174990, ProductCategory = "Computers", ImageUrl = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=500" },
                new Product { ProductId = 103, ProductName = "Fujifilm X-T50", ProductPrice = 129900, ProductCategory = "Cameras", ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500" },
                new Product { ProductId = 104, ProductName = "Apple Watch Ultra 2", ProductPrice = 89900, ProductCategory = "Wearables", ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500" }
            };
        }
    }
}