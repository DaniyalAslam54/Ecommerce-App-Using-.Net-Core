using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {

            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                product = new Product()
            };
             
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
            
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file) { 
            if(ModelState.IsValid)
            {
                if(file != null)
                {
                    string wwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwRootPath, @"images\Product");
                    using (var fileStream = new FileStream(Path.Combine(fileName, productPath), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.ImageUrl = @"images\Product" + fileName;
                }

                _unitOfWork.Product.Add(productVM.product);
                _unitOfWork.Save();
                TempData["success"] = "Product Added Successfullly";

                return RedirectToAction("Index");
            }
            return View();
        }
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if(id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? product = _unitOfWork.Product.Get(U=>U.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return View(product);
        //    }

             
        //}
        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product Updated Successfullly";

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? product = _unitOfWork.Product.Get(U => U.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }


            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfullly";
            return RedirectToAction("Index");

        }

    }
}
