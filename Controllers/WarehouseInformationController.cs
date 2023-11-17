using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    /// <summary>
    /// Класс WarehouseInformationController отвечает за работу со складами
    /// </summary>
    public class WarehouseInformationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarehouseInformationController(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Методо Index возвращает список складов.
        /// </summary>
        /// <returns>IEnumerable of WarehouseInformation</returns>
        public IActionResult Index()
        {
            IEnumerable<WarehouseInformation> objWarehouseInformationList = _db.WarehouseInformation;
            return View(objWarehouseInformationList);
        }

        // get - create
        public IActionResult Create()
        {
            return View();
        }

        // post - create
        /// <summary>
        /// Метод post - create создает склад.
        /// </summary>
        /// <param name="obj">Представление типа склад</param>
        /// <returns>Представление типа склад</returns>
        [HttpPost]
        public IActionResult Create(WarehouseInformation obj)
        {
            if (ModelState.IsValid)
            {
                _db.WarehouseInformation.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// Метод get - edit обновляет склад.
        /// </summary>
        /// <param name="id">Id склада</param>
        /// <returns>Представление типа склад для проверки</returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.WarehouseInformation.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// Метод post - edit обновляет склад.
        /// </summary>
        /// <param name="obj">Представление типа склад</param>
        /// <returns>Представление типа склад</returns>
        [HttpPost]
        public IActionResult Edit(WarehouseInformation obj)
        {
            if (ModelState.IsValid)
            {
                _db.WarehouseInformation.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// Метод get - delete удаляет склад.
        /// </summary>
        /// <param name="id">Id склада</param>
        /// <returns>Представление типа склад</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.WarehouseInformation.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// Метод post - delete удаляет склад.
        /// </summary>
        /// <param name="id">Id склада</param>
        /// <returns>Перенаправляет на страницу со всеми складами</returns>
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.WarehouseInformation.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.WarehouseInformation.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index"); ;
        }
    }
}
