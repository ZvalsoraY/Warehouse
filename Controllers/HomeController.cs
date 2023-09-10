using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Warehouse.Data;
using Warehouse.Migrations;
using Warehouse.Models;
using Warehouse.Models.ViewModels;

namespace Warehouse.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            //_logger = logger;
            _db = db;
        }
        public ActionResult Index(int? warehouse)
        {
            IQueryable<WarehouseProduct> warehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product);
                        
            if (warehouse != null && warehouse != 0)
            {
                warehouseProducts = warehouseProducts.Where(p => p.Id == warehouse);
            }            

            List<WarehouseInformation> warehouses = _db.WarehouseInformation.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            warehouses.Insert(0, new WarehouseInformation { Name = "Все", Id = 0 });

            WarehouseProductHomeVM plvm = new WarehouseProductHomeVM
            {
                WarehouseProducts = warehouseProducts.ToList(),
                WarehouseInformationSelectList = new SelectList(warehouses, "Id", "Name"),
            //    Positions = new SelectList(new List<string>()
            //{
            //    "Все",
            //    "Нападающий",
            //    "Полузащитник",
            //    "Защитник",
            //    "Вратарь"
            //})
            };
            return View(plvm);
        }
        //public IActionResult Index()
        //{
        //    WarehouseProductHomeVM warehouseProductHomeVM = new WarehouseProductHomeVM()
        //    {
        //        WarehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product),
        //        WarehouseInformationSelectList = _db.WarehouseInformation.ToList()
        //    };
        //    return View(warehouseProductHomeVM);          
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}