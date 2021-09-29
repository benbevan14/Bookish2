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

        public void BorrowBook(int userId, string isbn)
        {
            var bookId = GetBookIdFromIsbn(isbn);
            if (bookId == 0) 
            {
                Console.WriteLine("That book doesn't exist");
                return;
            }

            // Check whether that user has already borrowed that book
            if (Connection.QueryFirstOrDefault<int>($"SELECT * FROM copies WHERE bookId={bookId} AND BorrowedBy={userId}", 0) != 0)
            {
                Console.WriteLine("Sorry, that user already has a copy of that book on loan");
                return;
            }

            // Select the first copy from copies with the matching bookId where nobody else has borrowed that copy
            // Default of 0 if no copies are found
            int copyId = Connection.QueryFirstOrDefault<int>($"SELECT * FROM copies WHERE bookId={bookId} AND BorrowedBy IS NULL", 0);

            if (copyId != 0)
            {
                // Update the copy record to show which user it's currently borrowed by
                Connection.Execute($"UPDATE copies SET BorrowedBy={userId} WHERE Id={copyId}");
                Console.WriteLine("bookId: " + bookId + " successfully borrowed by userId: " + userId);
            }
            else
            {
                Console.WriteLine("Sorry, no copies of bookId: " + bookId + " are available");
            }
        }

        public void ReturnBook(int userId, string isbn)
        {
            var bookId = GetBookIdFromIsbn(isbn);
            if (bookId == 0)
            {
                Console.WriteLine("That book doesn't exist");
                return;
            }

            // Find the first copy of bookId that the user currently has borrowed
            // Default 0 if no copy is found
            int copyId =
                Connection.QueryFirstOrDefault<int>(
                    $"SELECT * FROM copies WHERE bookId={bookId} AND BorrowedBy={userId}", 0);

            if (copyId != 0)
            {
                // Update the copy record to show that the copy has been returned
                Connection.Execute($"UPDATE copies SET BorrowedBy=NULL WHERE Id={copyId}");
                Console.WriteLine("bookId: " + bookId + " was successfully returned by userId: " + userId);
            }
            else
            {
                Console.WriteLine("Sorry, userId: " + userId + " either doesn't currently have a copy of bookId: " + bookId + " or a copy of this book didn't exist in the first place");
            }
        }

        public IEnumerable<Copy> GetBorrowedBooks(int userId)
        {
            return Connection.Query<Copy>($"SELECT * FROM copies WHERE BorrowedBy={userId}");
        }

        // Book queries
        public IEnumerable<Book> GetAllBooks()
        {
            return Connection.Query<Book>("SELECT * FROM books");
        }

        public void AddBook(string title, string author, string isbn, int pages, int numCopies)
        {
            // Add this type of book to the repository
            string sql = $"INSERT INTO books VALUES('{title}', '{author}', '{isbn}', '{pages}');";
            int bookId = Connection.QuerySingle<int>(sql + "SELECT SCOPE_IDENTITY()");

            Console.WriteLine(title + " was added");

            for (int i = 0; i < numCopies; i++)
                AddCopy(bookId);
        }

        // Copy queries
        public IEnumerable<Copy> GetAllCopies()
        {
            return Connection.Query<Copy>("SELECT * FROM copies");
        }

        // Look up the book id of the copy we're adding and add it to the repository
        public void AddCopy(int bookId)
        {
            // Insert a copy into the repo with that book id
            string sql = $"INSERT INTO copies VALUES({bookId}, null, null)";
            Connection.Execute(sql);
            Console.WriteLine("A copy of bookId: " + bookId + " was added");
        }

        public int GetBookIdFromIsbn(string isbn)
        {
            // Return a default value of 0 if the isbn is not found
            return Connection.QueryFirstOrDefault<int>($"SELECT Id FROM books WHERE ISBN='{isbn}'", 0);
        }

        public string GetTitleFromIsbn(string isbn)
        {
            return Connection.QueryFirstOrDefault<string>($"SELECT Title FROM books WHERE ISBN='{isbn}'");
        }

        public int GetUserIdFromUsername(string username)
        {
            return Connection.QuerySingle<int>($"SELECT Id FROM users WHERE Username='{username}'");
        }

        public string GetTitleFromCopyId(int id)
        {
            return Connection.QuerySingle<string>($"SELECT Title FROM books WHERE Id={id}");
        }
    }
}
