using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _db.Product;
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
