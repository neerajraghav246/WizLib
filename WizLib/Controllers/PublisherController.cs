using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PublisherController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var publishers = _dbContext.Publishers.ToList();
            return View(publishers);
        }

        public IActionResult SaveUpdate(int? id)
        {
            Publisher publisher = new Publisher();
            if (id.HasValue)
            {
                publisher = _dbContext.Publishers.Find(id);
                if (publisher == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(publisher);
                }
            }
            else
            {
                return View(publisher);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Publisher_Id == 0)
                {
                    _dbContext.Publishers.Add(publisher);
                }
                else
                {
                    _dbContext.Publishers.Update(publisher);
                }
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }

        public IActionResult Delete(int id)
        {
            var publisher = _dbContext.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Publishers.Remove(publisher);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
      
    }
}
