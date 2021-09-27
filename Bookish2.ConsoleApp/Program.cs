using System;
using Bookish2.DataAccess;

namespace Bookish2.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BookRepository br = new BookRepository();

            br.AddBook("Tales to Give You Goosebumps", "Stine, R.L.", "0590134345", "barcodebarco3", 129, 3);

            foreach (Book b in br.GetAllBooks())
                Console.WriteLine(b);
            foreach (Copy c in br.GetAllCopies())
                Console.WriteLine(c);

            //br.BorrowBook(1, "0747532745");
            //br.BorrowBook(2, "074753274");

            //foreach (Copy c in br.GetAllCopies())
            //    Console.WriteLine(c);

            Console.WriteLine("\ndone");
            Console.ReadLine();
        }
    }
}
