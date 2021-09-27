using System;

namespace Bookish2.DataAccess
{
    public class Copy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowedBy { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            BookRepository br = new BookRepository();
            return $"Id: {Id}, BookId: {BookId}, BorrowedBy: {BorrowedBy}, DueDate: {DueDate}";
        }
    }
}