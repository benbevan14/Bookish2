using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookish2.DataAccess;

namespace Bookish2.Web.Models
{
    public class CopyModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowedBy { get; set; }
        public DateTime DueDate { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, BookId: {BookId}, BorrowedBy: {BorrowedBy}, DueDate: {DueDate}";
        }
    }
}