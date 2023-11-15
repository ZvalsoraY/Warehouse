using Microsoft.AspNetCore.Mvc;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
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

        // post - create
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

        // get - edit
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

        // post - edit
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

        // get - delete
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

        // post - delete
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
