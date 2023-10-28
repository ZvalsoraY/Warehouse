using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
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

        public IActionResult Index(int? warehouse)
        {
            IQueryable<WarehouseProduct> warehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product).Include(u => u.Product.ApplicationType);
                        
            if (warehouse != null && warehouse != 0)
            {
                warehouseProducts = warehouseProducts.Where(p => p.WarehouseId == warehouse);
            }            

            List<WarehouseInformation> warehouses = _db.WarehouseInformation.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            warehouses.Insert(0, new WarehouseInformation { Name = "Все", Id = 0 });

            WarehouseProductHomeVM plvm = new WarehouseProductHomeVM
            {
                WarehouseProducts = warehouseProducts.ToList().DistinctBy(u => u.Product.ApplicationTypeId),
                WarehouseInformationSelectList = new SelectList(warehouses, "Id", "Name")            
            };
            return View(plvm);
        }

        //[HttpPost]
        public IActionResult NumberItems(int warehouseProductId, int command)
        {
            var results = new List<int>();
            if (_db.WarehouseProduct.Find(warehouseProductId) == null)
            {
                return NotFound();
            }
            if(command >= 0 || _db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse > 0)
            {
                _db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse += command;
                _db.SaveChanges();
            }            
            results.Add(_db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse);
            return new JsonResult(results);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}