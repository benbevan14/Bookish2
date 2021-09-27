namespace Bookish2.DataAccess
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Barcode { get; set; }
        public int Pages { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Title} by {Author}.\nNumber of pages: {Pages}\nISBN: {ISBN}\nBarcode: {Barcode}";
        }
    }
}