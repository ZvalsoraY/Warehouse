using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.ViewModels;

namespace Warehouse.Controllers
{
    /// <summary>
    /// Класс WarehouseProductController отвечает за работу с хранением продукта на складе.
    /// </summary>
    public class WarehouseProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarehouseProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Методо Index возвращает список "продукт-склад".
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<WarehouseProduct> objList = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product);
            return View(objList);
        }

        /// <summary>
        /// Метод get - upsert для создания или обновления информации о "продукт-склад".
        /// </summary>
        /// <param name="id">Null для создания новой пары "продукт-склад", id для изменения текущей пары "продукт-склад"</param>
        /// <returns>Вовзращает информацию о паре "продукт-склад" для проверки</returns>
        public IActionResult Upsert(int? id)
        {

            WarehouseProductVM warehouseProductVM = new WarehouseProductVM()
            {
                WarehouseProduct = new WarehouseProduct(),
                WarehouseInformationSelectList = _db.WarehouseInformation.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ProductSelectList = _db.Product.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if (id == null)
            {
                return View(warehouseProductVM);
            }
            else
            {
                warehouseProductVM.WarehouseProduct = _db.WarehouseProduct.Find(id);
                if (warehouseProductVM.WarehouseProduct == null)
                {
                    return NotFound();
                }
                return View(warehouseProductVM);
            }
        }

        /// <summary>
        /// Метод post - upsert для создания или обновления информации о "продукт-склад".
        /// </summary>
        /// <param name="warehouseProductVM">Передается заполненная форма из get - upsert</param>
        /// <returns>Вовзращает информацию о паре "продукт-склад" для проверки</returns>
        [HttpPost]
        public IActionResult Upsert(WarehouseProductVM warehouseProductVM)
        {
            if (ModelState.IsValid)
            {
                if (warehouseProductVM.WarehouseProduct.Id == 0)
                {
                    //Creating
                    _db.WarehouseProduct.Add(warehouseProductVM.WarehouseProduct);
                }
                else
                {
                    //updating
                    _db.WarehouseProduct.Update(warehouseProductVM.WarehouseProduct);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            warehouseProductVM.WarehouseInformationSelectList = _db.WarehouseInformation.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            warehouseProductVM.ProductSelectList = _db.Product.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(warehouseProductVM);
        }


        /// <summary>
        /// Метод get - delete для удаления продукта.
        /// </summary>
        /// <param name="id">Id удаляемой пары "продукт-склад"</param>
        /// <returns>Вовзращает информацию о паре "продукт-склад" для проверки</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            WarehouseProduct warehouseProduct = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product).FirstOrDefault(u => u.Id == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }
            return View(warehouseProduct);
        }

        /// <summary>
        /// Метод post - delete для удаления продукта.
        /// </summary>
        /// <param name="id">Id удаляемой пары "продукт-склад"</param>
        /// <returns>Возвращает на страницу с таблицей "продукт-склад"</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.WarehouseProduct.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.WarehouseProduct.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index"); ;
        }
    }
}
