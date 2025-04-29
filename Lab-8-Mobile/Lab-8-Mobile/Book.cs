using SQLite;

namespace MyApp.Data
{
    [Table("BookLibrary")]
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull] public string ISBN { get; set; }
        [NotNull] public string Title { get; set; }
        [NotNull] public string Author { get; set; }
        [NotNull] public string PublishDate { get; set; }
        public string Genre { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
