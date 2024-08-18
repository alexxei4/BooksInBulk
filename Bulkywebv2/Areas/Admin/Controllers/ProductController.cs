using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bulkywebv2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                // This will handle the case for a new product
                return View(productVM);
            }
            else
            {
                // Fetch the product if the ID is provided
                productVM.Product = _unitOfWork.Product.GetAll().FirstOrDefault(u => u.Id == id);
                if (productVM.Product == null)
                {
                    // Handle the case where the product with the given ID doesn't exist
                    return NotFound();
                }
                else
                {
                    // Return the view with the existing product
                    return View(productVM);
                }
            }
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            
            }

          
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductfromDb = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (ProductfromDb == null)
            {
                return NotFound();
            }

            return View(ProductfromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? ProductfromDb = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (ProductfromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(ProductfromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}

