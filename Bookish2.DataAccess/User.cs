namespace Bookish2.DataAccess
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} ({Username})";
        }
    }
}