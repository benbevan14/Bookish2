using System;
using Bookish2.DataAccess;

namespace Bookish2.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BookRepository br = new BookRepository();

            foreach (User user in br.GetUsersWithSurname("Bevan"))
            {
                Console.WriteLine(user);
            }

            Console.WriteLine();

            foreach (Book book in br.GetAllBooks())
            {
                Console.WriteLine(book);
            }

            Console.WriteLine();

            foreach (Copy copy in br.GetAllCopies())
            {
                Console.WriteLine(copy);
            }

            Console.WriteLine("\ndone");
            Console.ReadLine();
        }
    }
}
