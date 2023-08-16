using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class WarehouseInformationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarehouseInformationController(ApplicationDbContext db)
        {
            _db = db;
        }
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
            //return View();
        }

        // get - edit
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

        // post - edit
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        // get - delete
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

        // post - delete
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
