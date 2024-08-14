using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bulkywebv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _ProductRepo;

        public ProductController(IProductRepository db)
        {
            _ProductRepo = db;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _ProductRepo.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
           
            if (ModelState.IsValid)
            {
                _ProductRepo.Add(obj);
                _ProductRepo.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductfromDb = _ProductRepo.GetFirstOrDefault(x => x.Id == id);
            if (ProductfromDb == null)
            {
                return NotFound();
            }

            return View(ProductfromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _ProductRepo.Update(obj);
                _ProductRepo.Save();
                TempData["success"] = "Product edited successfully";
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

            Product? ProductfromDb = _ProductRepo.GetFirstOrDefault(x => x.Id == id);
            if (ProductfromDb == null)
            {
                return NotFound();
            }

            return View(ProductfromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? ProductfromDb = _ProductRepo.GetFirstOrDefault(x => x.Id == id);
            if (ProductfromDb == null)
            {
                return NotFound();
            }
            _ProductRepo.Remove(ProductfromDb);
            _ProductRepo.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
