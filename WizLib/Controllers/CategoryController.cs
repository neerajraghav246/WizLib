using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult SaveUpdate(int? id)
        {
            Category category = new Category();
            if (id.HasValue)
            {
                category = _dbContext.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(category);
                }
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Category_Id == 0)
                {
                    _dbContext.Categories.Add(category);
                }
                else
                {
                    _dbContext.Categories.Update(category);
                }
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult CreateMultiple2()
        {
            for (int i = 0; i < 2; i++)
            {
                _dbContext.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            for (int i = 0; i < 5; i++)
            {
                _dbContext.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {
            var categories = _dbContext.Categories.OrderByDescending(x => x.Category_Id).Take(2).ToList();
            _dbContext.Categories.RemoveRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            var categories = _dbContext.Categories.OrderByDescending(x => x.Category_Id).Take(5).ToList();
            _dbContext.Categories.RemoveRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
