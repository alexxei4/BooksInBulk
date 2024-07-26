﻿using Bulkywebv2.Data;
using Bulkywebv2.Models;
using Microsoft.AspNetCore.Mvc;
//using builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
namespace Bulkywebv2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() { 
            return View();
        }
      
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name. ");
                
            }
            if ( obj.Name!=null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "This is an invalid value. ");

            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Edit(int? id)
        {
            if ( id==null || id == 0 ) {
                return NotFound();
            }

            Category? categoryfromDb = _db.Categories.Find(id);
            //Category? categoryfromDb1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryfromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");

            }

            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryfromDb = _db.Categories.Find(id);
           

            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }


        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id); 
            if (obj == null) 
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
