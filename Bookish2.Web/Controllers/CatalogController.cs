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
        // GET: Catalog
        public ActionResult Index()
        {
            BookRepository br = new BookRepository();
            var books = br.GetAllBooks()
                .Select(b => new BookModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Pages = b.Pages
                })
                .ToList();

            return View(books);
        }
    }
}