﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Models.ViewModels;

namespace Warehouse.Controllers
{
    public class WarehouseProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarehouseProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<WarehouseProduct> objList = _db.WarehouseProduct.Include(u => u.WarehouseInformation).Include(u => u.Product);
            return View(objList);
        }        
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

        // post - upsert
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


        // get - delete
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

        // post - delete
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
