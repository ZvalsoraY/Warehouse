using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Warehouse.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product.Include(u => u.ApplicationType);
            return View(objList);
        }

        // get - upsert
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

        // post - upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
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


        // get - delete
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

        // post - delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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


        //private readonly ApplicationDbContext _db;
        ////private readonly IWebHostEnvironment _webHostEnvironment;

        //public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        //{
        //    _db = db;            
        // //   _webHostEnvironment = webHostEnvironment;
        //}
        ////IEnumerable<ApplicationType> applicationTypes = _db.ApplicationType.GetAll<ApplicationType>();
        //public IActionResult Index()
        //{
        //    IEnumerable<Product> objList = _db.Product.Include(u => u.ApplicationType);
        //    //IEnumerable<Product> objList = _db.Product;

        //    //foreach (var obj in objList)
        //    //{
        //    //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CateroryId);
        //    //    obj.ApplicationType = _db.ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
        //    //}

        //    return View(objList);
        //}


        //public IActionResult Create()
        //{
        //    IEnumerable<ApplicationType> applicationTypes = _db.ApplicationType;
        //    ViewBag.ApplicationTypes = new SelectList(applicationTypes, "Id", "Name");
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationType applicationType = _db.ApplicationType.FirstOrDefault(c => c.Id == product.ApplicationTypeId);
        //        //return $"Добавлен новый элемент: {product.Name} ({company?.Name})";
        //        _db.Product.Add(product);
        //        _db.SaveChanges();
        //        //return $"Добавлен новый элемент: {product.Name} ({product?.Name})";
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //    //return RedirectToAction("Index");
        //    //return View(product);
        //    //if (ModelState.IsValid)
        //    //{
        //    //    _db.ApplicationType.Add(obj);
        //    //    _db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}
        //    //return View(obj);
        //}


        //// get - upsert
        //public IActionResult Upsert(int? id)
        //{
        //    //IEnumerable<ApplicationType> applicationTypes = _db.ApplicationType;
        //    ViewBag.ApplicationTypeSelectList = new SelectList(_db.ApplicationType, "Id", "Name");
        //    ProductVM productVM = new ProductVM();
        //    //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
        //    //{
        //    //    Text = i.Name,
        //    //    Value = i.Id.ToString()
        //    //});

        //    ////ViewBag.CategoryDropDown = CategoryDropDown;
        //    //ViewData["CategoryDropDown"] = CategoryDropDown;

        //    //Product product = new Product();
        //    //ProductVM productVM = new ProductVM()
        //    //{
        //    //    Product = new Product(),
        //    //    //CategorySelectList = _db.Category.Select(i => new SelectListItem
        //    //    //{
        //    //    //    Text = i.Name,
        //    //    //    Value = i.Id.ToString()
        //    //    //}),
        //    //    //ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
        //    //    //{
        //    //    //    Text = i.Name,
        //    //    //    Value = i.Id.ToString()
        //    //    //})
        //    //    ApplicationTypeSelectList = new SelectList(_db.ApplicationType, "Id", "Name")

        //    //};

        //    if (id == null)
        //    {
        //        //this is fo create
        //        return View(productVM);
        //    }
        //    else
        //    {
        //        productVM.Product = _db.Product.Find(id);
        //        //productVM.Product = _db.Product.FirstOrDefault(c => c.Id == productVM.Product.Id); ;
        //        if (productVM.Product == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(productVM);
        //    }
        //}

        //// post - upsert
        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Upsert(ProductVM productVM)
        //{
        //    //_db.Product.Add(productVM.Product);
        //    if (ModelState.IsValid)
        //    {
        //        //var files = HttpContext.Request.Form.Files;
        //        //string webRootPath = _webHostEnvironment.WebRootPath;
        //        if (productVM.Product.Id == 0)
        //        {
        //            //Creating
        //            //string upload = webRootPath + WC.ImagePath;
        //            //string fileName = Guid.NewGuid().ToString();
        //            //string extantion = Path.GetExtension(files[0].FileName);
        //            //using (var fileStream = new FileStream(Path.Combine(upload, fileName + extantion), FileMode.Create))
        //            //{
        //            //    files[0].CopyTo(fileStream);
        //            //}
        //            //productVM.Product.Image = fileName + extantion;

        //            _db.Product.Add(productVM.Product);
        //        }
        //        else
        //        {
        //            //updating
        //            var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(x => x.Id == productVM.Product.Id);

        //            //if (files.Count > 0)
        //            //{
        //            //    string upload = webRootPath + WC.ImagePath;
        //            //    string fileName = Guid.NewGuid().ToString();
        //            //    string extantion = Path.GetExtension(files[0].FileName);

        //            //    var oldFile = Path.Combine(upload, objFromDb.Image);

        //            //    if (System.IO.File.Exists(oldFile))
        //            //    {
        //            //        System.IO.File.Delete(oldFile);
        //            //    }

        //            //    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extantion), FileMode.Create))
        //            //    {
        //            //        files[0].CopyTo(fileStream);
        //            //    }

        //            //    productVM.Product.Image = fileName + extantion;
        //            //}
        //            //else
        //            //{
        //            //    productVM.Product.Image = objFromDb.Image;
        //            //}

        //            _db.Product.Update(productVM.Product);
        //        }

        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    //productVM.CategorySelectList = _db.Category.Select(i => new SelectListItem
        //    //{
        //    //    Text = i.Name,
        //    //    Value = i.Id.ToString()
        //    //});
        //    productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
        //    {
        //        Text = i.Name,
        //        Value = i.Id.ToString()
        //    });
        //    return View(productVM);
        //}


        //// get - delete
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product product = _db.Product.Include(u => u.ApplicationType).FirstOrDefault(u => u.Id == id);
        //    //product.Category = _db.Category.Find(product.CateroryId);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //// post - delete
        //[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public IActionResult DeletePost(int? id)
        //{
        //    var obj = _db.Product.Find(id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }

        //    //string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
        //    //var oldFile = Path.Combine(upload, obj.Image);

        //    //if (System.IO.File.Exists(oldFile))
        //    //{
        //    //    System.IO.File.Delete(oldFile);
        //    //}

        //    _db.Product.Remove(obj);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index"); ;
    //}
   // }
    ////////////////////////////////////////////////////////////////////////////////
    //public class ProductController : Controller
    //{
    //    private readonly ApplicationDbContext _db;
    //    //private readonly IWebHostEnvironment _webHostEnvironment;

    //    public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    //    {
    //        _db = db;
    //        //_webHostEnvironment = webHostEnvironment;
    //    }
    //    public IActionResult Index()
    //    {
    //        IEnumerable<Product> objList = _db.Product.Include(u => u.ApplicationType);
    //        return View(objList);
    //    }

    //    // get - upsert
    //    public IActionResult Upsert(int? id)
    //    {
    //        //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
    //        //{
    //        //    Text = i.Name,
    //        //    Value = i.Id.ToString()
    //        //});

    //        ////ViewBag.CategoryDropDown = CategoryDropDown;
    //        //ViewData["CategoryDropDown"] = CategoryDropDown;

    //        //Product product = new Product();

    //        ProductVM productVM = new ProductVM()
    //        {
    //            Product = new Product(),
    //            ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
    //            {
    //                Text = i.Name,
    //                Value = i.Id.ToString()
    //                //new ApplicationType { Text = "Turkey", Value = "1" }
    //                //Name = "Turkey",
    //                //Value = "1"
    //            })

    //        };

    //        if (id == null)
    //        {
    //            //this is fo create
    //            return View(productVM);
    //            //return View(); 
    //        }
    //        else
    //        {
    //            productVM.Product = _db.Product.Find(id);
    //            if (productVM.Product == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(productVM);
    //        }
    //    }

    //    // post - upsert
    //    [HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    public IActionResult Upsert(ProductVM productVM)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            //var files = HttpContext.Request.Form.Files;
    //            //string webRootPath = _webHostEnvironment.WebRootPath;
    //            if (productVM.Product.Id == 0)
    //            {
    //                //Creating
    //                //string upload = webRootPath + WC.ImagePath;
    //                //string fileName = Guid.NewGuid().ToString();
    //                //string extantion = Path.GetExtension(files[0].FileName);
    //                //using (var fileStream = new FileStream(Path.Combine(upload, fileName + extantion), FileMode.Create))
    //                //{
    //                //    files[0].CopyTo(fileStream);
    //                //}
    //                //productVM.Product.Image = fileName + extantion;

    //                _db.Product.Add(productVM.Product);
    //            }
    //            else
    //            {
    //                //updating
    //                var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(x => x.Id == productVM.Product.Id);

    //                //if (files.Count > 0)
    //                //{
    //                //    string upload = webRootPath + WC.ImagePath;
    //                //    string fileName = Guid.NewGuid().ToString();
    //                //    string extantion = Path.GetExtension(files[0].FileName);

    //                //    var oldFile = Path.Combine(upload, objFromDb.Image);

    //                //    if (System.IO.File.Exists(oldFile))
    //                //    {
    //                //        System.IO.File.Delete(oldFile);
    //                //    }

    //                //    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extantion), FileMode.Create))
    //                //    {
    //                //        files[0].CopyTo(fileStream);
    //                //    }

    //                //    productVM.Product.Image = fileName + extantion;
    //                //}
    //                //else
    //                //{
    //                //    productVM.Product.Image = objFromDb.Image;
    //                //}

    //                _db.Product.Update(productVM.Product);
    //            }

    //            _db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        //productVM.CategorySelectList = _db.Category.Select(i => new SelectListItem
    //        //{
    //        //    Text = i.Name,
    //        //    Value = i.Id.ToString()
    //        //});
    //        productVM.ApplicationTypeSelectList = _db.ApplicationType.Select(i => new SelectListItem
    //        {
    //            Text = i.Name,
    //            Value = i.Id.ToString()
    //        });
    //        return View(productVM);
    //    }


    //    // get - delete
    //    public IActionResult Delete(int? id)
    //    {
    //        if (id == null || id == 0)
    //        {
    //            return NotFound();
    //        }

    //        Product product = _db.Product.Include(u => u.ApplicationType).FirstOrDefault(u => u.Id == id);
    //        //product.Category = _db.Category.Find(product.CateroryId);
    //        if (product == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(product);
    //    }

    //    // post - delete
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult DeletePost(int? id)
    //    {
    //        var obj = _db.Product.Find(id);
    //        if (obj == null)
    //        {
    //            return NotFound();
    //        }

    //        //string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
    //        //var oldFile = Path.Combine(upload, obj.Image);

    //        //if (System.IO.File.Exists(oldFile))
    //        //{
    //        //    System.IO.File.Delete(oldFile);
    //        //}

    //        _db.Product.Remove(obj);
    //        _db.SaveChanges();
    //        return RedirectToAction("Index"); ;
    //    }
    //}
//}