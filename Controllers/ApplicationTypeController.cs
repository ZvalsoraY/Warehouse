using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    /// <summary>
    /// Класс ApplicationTypeController отвечает за работу с типами продуктов
    /// </summary>
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Методо Index возвращает список типов продукта.
        /// </summary>
        /// <returns>IEnumerable of ApplicationType</returns>
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objApplicationTypeList = _db.ApplicationType;
            return View(objApplicationTypeList);
        }

        // get - create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Метод post - create создает тип продукта.
        /// </summary>
        /// <param name="obj"> Представление типа продукта</param>
        /// <returns> Представление типа продукта</returns>
        [HttpPost]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
            //return View();
        }

        /// <summary>
        /// Метод get - edit для обновления информации о типе продукта. 
        /// </summary>
        /// <param name="id">Id продукта</param>
        /// <returns>Представление тип продукта</returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// Метод post - edit для обновления информации о типе продукта. 
        /// </summary>
        /// <param name="obj">Представление типа продукта</param>
        /// <returns>Представление типа продукта</returns>
        [HttpPost]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// Метод get - delete для удаления типа продукта.
        /// </summary>
        /// <param name="id">Id продукта</param>
        /// <returns>Представление типа продукта</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// Метод post - delete для удаления типа продукта. 
        /// </summary>
        /// <param name="id">Id продукта</param>
        /// <returns>Перенаправляет на страницу со всеми тиами продукта</returns>
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ApplicationType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index"); ;
        }
    }
}
