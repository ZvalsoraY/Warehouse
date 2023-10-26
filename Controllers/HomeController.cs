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
            IQueryable<WarehouseProduct> warehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product);
                        
            if (warehouse != null && warehouse != 0)
            {
                warehouseProducts = warehouseProducts.Where(p => p.WarehouseId == warehouse);
            }            

            List<WarehouseInformation> warehouses = _db.WarehouseInformation.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            warehouses.Insert(0, new WarehouseInformation { Name = "Все", Id = 0 });

            WarehouseProductHomeVM plvm = new WarehouseProductHomeVM
            {
                WarehouseProducts = warehouseProducts.ToList(),
                WarehouseInformationSelectList = new SelectList(warehouses, "Id", "Name")
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

        //[HttpPost]
        public IActionResult NumberItems(int warehouseProductId, int command)
        {
            var results = new List<int>();
            //warehouseProduct = _db.WarehouseProduct.Find(warehouseProductId);
            if (_db.WarehouseProduct.Find(warehouseProductId) == null)
            {
                return NotFound();
            }
            if(command >= 0 || _db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse > 0)
            {
                _db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse += command;
                _db.SaveChanges();
            }
            //if (operation == "-")
            //{
                //_db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse -= 1;
                //_db.SaveChanges();
                results.Add(_db.WarehouseProduct.Find(warehouseProductId).NumbProdInWarehouse);

            //}
            //else
            //{
            //}
            return new JsonResult(results);
        }
        //public async Task<IActionResult> Index()
        //{
        //    var blog = _db.WarehouseProduct.Find(id);
        //    blog.NumbProdInWarehouse = value;
        //    _db.SaveChanges();
        //    return Json("ok");
        //}
        //private async Task OnChangeQuantity(ProductWithQuantity product, int quantityDiff)
        //{
        //    var dto = new StorageIncreaseProductQuantityDto(product.ProductId, quantityDiff);

        //    var serializedDto = JsonSerializer.Serialize(dto);
        //    var requestContent = new StringContent(serializedDto, Encoding.UTF8, "application/json-patch+json");

        //    var response = await WarehouseApi.PatchAsync($"/api/storage/{_storageId}/increaseProductQuantity", requestContent);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        if (_hubConnection is not null)
        //        {
        //            await _hubConnection.SendAsync("SendQuantityChanged", _storageId, product.ProductId, quantityDiff);
        //        }

        //        var storageDto = await response.Content.ReadFromJsonAsync<StorageDto>();
        //        var changedProduct = storageDto.Products.First(p => p.ProductId == product.ProductId);

        //        var indexOfProduct = _products.IndexOf(product);
        //        _products[indexOfProduct] = product with { Quantity = changedProduct.Quantity };
        //    }
        //    else
        //        Console.WriteLine(await response.Content.ReadAsStringAsync());
        //}
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Blogs.ToListAsync());
        //}
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