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

        public IEnumerable<User> GetAllUsers()
        {
            return Connection.Query<User>("SELECT * FROM users");
        }
    }
}
