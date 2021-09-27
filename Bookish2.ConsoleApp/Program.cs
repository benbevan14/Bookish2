using System;
using Bookish2.DataAccess;

namespace Bookish2.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BookRepository br = new BookRepository();

            Console.WriteLine("\ndone");
            Console.ReadLine();
        }
    }
}
