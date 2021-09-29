using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookish2.DataAccess;
using Bookish2.Web.Models;

namespace Bookish2.Web.Controllers
{
    public class CatalogController : Controller
    {
        private BookRepository br = new BookRepository();
        // GET: Catalog
        public ActionResult Index()
        {
            var books = br.GetAllBooks()
                .Select(b => new BookModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Pages = b.Pages
                })
                .OrderBy(model => model.Title)
                .ToList();

            return View(books);
        }

        public ActionResult UserBooks()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name.Split('@')[0];
            if (username == "")
            {
                return View("Error");
            }

            int userId = br.GetUserIdFromUsername(username);
            var borrowed = br.GetBorrowedBooks(userId)
                .Select(c => new CopyModel()
                {
                    Id = c.Id,
                    BookId = c.BookId,
                    BorrowedBy = c.BorrowedBy,
                    DueDate = c.DueDate,
                    Title = br.GetTitleFromCopyId(c.BookId),
                })
                .ToList();
            return View(borrowed);
        }

        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(BookModel model)
        {
            br.AddBook(model.Title, model.Author, model.ISBN, model.Pages, 1);

            //ModelState.Clear();

            return RedirectToAction("Form");
        }
    }
}