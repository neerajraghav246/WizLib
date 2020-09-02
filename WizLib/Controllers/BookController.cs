using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var books = _dbContext.Books.ToList();
            return View(books);
        }

        public IActionResult SaveUpdate(int? id)
        {
            Author author = new Author();
            if (id.HasValue)
            {
                author = _dbContext.Authors.Find(id);
                if (author == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(author);
                }
            }
            else
            {
                return View(author);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Author_Id == 0)
                {
                    _dbContext.Authors.Add(author);
                }
                else
                {
                    _dbContext.Authors.Update(author);
                }
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        public IActionResult Delete(int id)
        {
            var author = _dbContext.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Authors.Remove(author);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
