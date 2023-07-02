﻿using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {

            _db = db;
        }
        public IActionResult Index()
        {

            List<Category> categories = _db.Categories.ToList();
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

            if(ModelState.IsValid)
            {

                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Added Successfullly";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {

            if(id == null  || id == 0) {
                return NotFound();
            }

            Category? category = _db.Categories.Find(id);
            if(category == null)
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

                _db.Categories.Update(category);
                _db.SaveChanges();
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

            Category? category = _db.Categories.Find(id);
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


        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfullly";
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{

            //    _db.Categories.Update(category);
            //    _db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View();
        }
    }
}