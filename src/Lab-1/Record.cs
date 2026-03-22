namespace Lab_1
{
    public class Record
    {
        public int BookId { get; set; }
        public string Title { get; set; }   
        public string Author { get; set; }
        public int Year { get; set; }

        public Record(int bookId, string title, string author, int year)
        {
            BookId = bookId;
            Title = title;
            Author = author;
            Year = year;
        }
    }
}
