﻿using BookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;

        public CategoryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        public ViewResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }



        public ActionResult Delete(int id)
        {
            var author = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (author == null)
                return HttpNotFound();
            else
            {
                _context.Categories.Remove(author);
                _context.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category category)
        {
            if (!ModelState.IsValid)
            {
                if (category.Id == 0)
                    return View("create", category);
                else
                    return View("Edit", category);
            }
            if (category.Id == 0)
                _context.Categories.Add(category);
            else
            {
                var categoryInDb = _context.Categories.Single(c => c.Id == category.Id);
                categoryInDb.Name = category.Name;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}