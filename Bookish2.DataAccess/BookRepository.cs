using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Bookish2.DataAccess
{
    public class BookRepository
    {
        public string ConnectionString { get; set; }
        public SqlConnection Connection { get; set; }

        public BookRepository()
        {
            ConnectionString = @"Server=localhost;Database=bookish;Trusted_Connection=True;";
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        // User queries
        public IEnumerable<User> GetAllUsers()
        {
            return Connection.Query<User>("SELECT * FROM users");
        }

        public IEnumerable<User> GetUsersWithSurname(string surname)
        {
            return Connection.Query<User>($"SELECT * FROM users WHERE LastName='{surname}'");
        }

        public void BorrowBook(int userId, string isbn)
        {

        }

        // Book queries
        public IEnumerable<Book> GetAllBooks()
        {
            return Connection.Query<Book>("SELECT * FROM books");
        }

        public void AddBook(string title, string author, string isbn, string barcode, int pages)
        {
            // Add this type of book to the repository
            string sql = $"INSERT INTO books VALUES('{title}', '{author}', '{isbn}', '{barcode}', '{pages}')";
            Connection.Execute(sql);
        }

        // Copy queries
        public IEnumerable<Copy> GetAllCopies()
        {
            return Connection.Query<Copy>("SELECT * FROM copies");
        }

        // Look up the book id of the copy we're adding and add it to the repository
        public void AddCopy(string isbn)
        {
            var bookId = Connection.Query<int>($"SELECT * FROM books WHERE ISBN='{isbn}'").First();
            Console.WriteLine("bookId: " + bookId);

            // Insert a copy into the repo with that book id
            string sql = $"INSERT INTO copies VALUES({bookId}, null, null)";
            Connection.Execute(sql);
        }
    }
}
