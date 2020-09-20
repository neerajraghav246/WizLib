using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;

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
            var books = _dbContext.Books.Include(u => u.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).ToList();
            //foreach (var book in books)
            //{
            //    _dbContext.Entry(book).Reference(b => b.Publisher).Load();
            //}
            return View(books);
        }

        public IActionResult SaveUpdate(int? id)
        {
            BookVM book = new BookVM();
            book.PublisherList = _dbContext.Publishers.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });

            if (id.HasValue)
            {
                book.Book = _dbContext.Books.Find(id);
                if (book.Book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUpdate(BookVM bookVM)
        {
            ModelState["Book.Book_Id"].Errors.Clear();
            ModelState["Book.Book_Id"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                if (bookVM.Book.Book_Id == 0)
                {
                    _dbContext.Books.Add(bookVM.Book);
                }
                else
                {
                    _dbContext.Books.Update(bookVM.Book);
                }
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(bookVM);
        }

        public IActionResult Details(int? id)
        {
            BookVM book = new BookVM();

            if (id.HasValue)
            {
                book.Book = _dbContext.Books.Include(u => u.BookDetail).FirstOrDefault(b => b.Book_Id == id);
                //book.Book.BookDetail = _dbContext.BookDetails.Find(book.Book.BookDetail_Id);
                if (book.Book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM bookVM)
        {
            if (!bookVM.Book.BookDetail_Id.HasValue)
            {
                _dbContext.BookDetails.Add(bookVM.Book.BookDetail);
                _dbContext.SaveChanges();
                var BookFromDB = _dbContext.Books.FirstOrDefault(r => r.Book_Id == bookVM.Book.Book_Id);
                BookFromDB.BookDetail_Id = bookVM.Book.BookDetail.BookDetail_Id;
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.BookDetails.Update(bookVM.Book.BookDetail);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM bookAuthorVM = new BookAuthorVM
            {
                BookAuthorList = _dbContext.BookAuthors.Include(u => u.Book).Include(u => u.Author).Where(u => u.Book_Id == id),
                BookAuthor = new BookAuthor
                {
                    Book_Id = id
                },
                Book = _dbContext.Books.Find(id)
            };

            var tempListOfAssignedAuthors = bookAuthorVM.BookAuthorList.Select(u => u.Author_Id).ToList();
            //NOT IN
            var unAssignedAuthors = _dbContext.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id));
            //projection
            bookAuthorVM.AuthorList = unAssignedAuthors.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = x.FullName,
                Value = x.Author_Id.ToString()
            }).ToList();
            return View(bookAuthorVM);

        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _dbContext.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            var bookId = bookAuthorVM.Book.Book_Id;
            var bookAuthor = _dbContext.BookAuthors.FirstOrDefault(u => u.Book_Id == bookId && u.Author_Id == authorId);
            _dbContext.BookAuthors.Remove(bookAuthor);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }
    }
}
