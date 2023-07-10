using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Added Successfullly";
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

            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return View(category);
            }
            //Category category = _db.Categories.FirstOrDefault(u=>u.Id == id );
            //Category category = _db.Categories.Where(u=>u.Id == id ).FirstOrDefault();

        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfullly";
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

            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return View(category);
            }
            //Category category = _db.Categories.FirstOrDefault(u=>u.Id == id );
            //Category category = _db.Categories.Where(u=>u.Id == id ).FirstOrDefault();

        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }


            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfullly";
            return RedirectToAction("Index");
             
        }
    }
}
