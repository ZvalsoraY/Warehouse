using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Models.ViewModels;

namespace Warehouse.Controllers
{
    [Route("api/WarehouseAPI")]
    [ApiController]
    public class WarehouseAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public WarehouseAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async ActionResult<IEnumerable<WarehouseProduct>> GetWarehouseProducts()
        {
            return Ok(_db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product).Include(u => u.Product.ApplicationType).ToList());

        }

        [HttpGet("{warehouseId:int}", Name = "GetWarehouseProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async ActionResult<IEnumerable<WarehouseProduct>> GetWarehouseProducts(int warehouseId)
        {
            IQueryable<WarehouseProduct> warehouseProducts = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product).Include(u => u.Product.ApplicationType).OrderBy(d => d.Product.ApplicationType.Id);

            if (warehouseId != null && warehouseId != 0)
            {
                warehouseProducts = warehouseProducts.Where(p => p.WarehouseId == warehouseId);
            }

            List<WarehouseInformation> warehouses = _db.WarehouseInformation.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            warehouses.Insert(0, new WarehouseInformation { Name = "Все", Id = 0 });

            WarehouseProductHomeVM plvm = new WarehouseProductHomeVM
            {
                WarehouseProducts = warehouseProducts.ToList().DistinctBy(u => u.Product.ApplicationTypeId).Take(5),
                WarehouseInformationSelectList = new SelectList(warehouses, "Id", "Name")
            };
            
            return Ok(plvm);
        }

        [HttpPut("{warehouseProductId:int}", Name = "IncreaseWarehouseProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IncreaseWarehouseProduct(long id, StorageIncreaseProductQuantityDto inputDto)
        {
            var response = await _storageService.IncreaseProductQuantity(id, inputDto);

            if (!response.Success) return BadRequest(response.ErrorMessage);

            return Ok(response.Resource);
        }
    }
}
