using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.ViewModels;


namespace Warehouse.Controllers
{
    /// <summary>
    /// Главная страница, приветствующая пользователя.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Метод Index для отображения данных на странице
        /// </summary>
        /// <param name="warehouse">id склада, для которого выводятся данные</param>
        /// <returns>WarehouseProductHomeVM данные для отображения на странице</returns>
        public IActionResult Index(int? warehouse)
        {
            IQueryable<WarehouseProduct> warehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product).Include(u => u.Product.ApplicationType).OrderBy(d => d.Product.ApplicationType.Id);
                        
            if (warehouse != null && warehouse != 0)
            {
                warehouseProducts = warehouseProducts.Where(p => p.WarehouseId == warehouse);
            }            

            List<WarehouseInformation> warehouses = _db.WarehouseInformation.ToList();
            warehouses.Insert(0, new WarehouseInformation { Name = "Все", Id = 0 });

            List<Storekeeper> storekeepers = _db.Storekeeper.ToList();
            storekeepers.Insert(0, new Storekeeper { Name = "Все", Id = 0 });

            WarehouseProductHomeVM plvm = new WarehouseProductHomeVM
            {
                WarehouseProducts = warehouseProducts.ToList().DistinctBy(u => u.Product.ApplicationTypeId).Take(5),
                WarehouseInformationSelectList = new SelectList(warehouses, "Id", "Name"),
                StorekeeperSelectList = new SelectList(storekeepers, "Id", "Name")
            };
            return View(plvm);
        }

        /// <summary>
        /// Метод NumberItems для изменения количества товара на складе
        /// </summary>
        /// <param name="warehouseProductId"> ID пары товар-склад</param>
        /// <param name="changeNumber">значение изменения количества</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NumberItems(int warehouseProductId, int changeNumber)
        {
            var results = new List<int>();
            var warehouseProduct = _db.WarehouseProduct.Find(warehouseProductId);
            if (warehouseProduct == null)
            {
                return NotFound();
            }
            if(changeNumber >= 0 || warehouseProduct.NumbProdInWarehouse > 0)
            {
                warehouseProduct.NumbProdInWarehouse += changeNumber;
                _db.SaveChanges();
            }            
            results.Add(warehouseProduct.NumbProdInWarehouse);
            return new JsonResult(results);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}