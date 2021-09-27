using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookish2.Web.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Title} by {Author}.\nNumber of pages: {Pages}\nISBN: {ISBN}";
        }
    }
}