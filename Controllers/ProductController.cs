using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.ViewModels;


namespace Warehouse.Controllers
{
    /// <summary>
    /// Класс ProductController отвечает за работу с продуктами
    /// </summary>
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Методо Index возвращает список продуктов и их тип.
        /// </summary>
        /// <returns>IEnumerable of Product</returns>
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product.Include(u => u.ApplicationType);
            return View(objList);
        }

        /// <summary>
        /// Метод get - upsert для создания или обновления информации о продукте.
        /// </summary>
        /// <param name="id"> Null для создания нового продукта, id для изменения текущего</param>
        /// <returns>Вовзращает информацию о продукте для проверки</returns>
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if (id == null)
            {
                //this is fo create
                return View(productVM);
            }
            else
            {
                productVM.Product = _db.Product.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }


        /// <summary>
        /// Метод post - upsert для создания или обновления информации о продукте.
        /// </summary>
        /// <param name="productVM">Передается заполненная форма из get - upsert</param>
        /// <returns>Вовзращает информацию о продукте для проверки</returns>
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (productVM.Product.Id == 0)
                {
                    //Creating
                    _db.Product.Add(productVM.Product);
                }
                else
                {
                    //updating
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(x => x.Id == productVM.Product.Id);
                    _db.Product.Update(productVM.Product);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(productVM);
        }


        /// <summary>
        /// Метод get - delete для удаления продукта.
        /// </summary>
        /// <param name="id"> Id удаляемого продукта</param>
        /// <returns> Вовзращает информацию о продукте для проверки</returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _db.Product.Include(u => u.ApplicationType).FirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        
        /// <summary>
        /// Метод post - delete для удаления продукта.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Возвращает на страницу с продуктами</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index"); ;
        }
    }
}