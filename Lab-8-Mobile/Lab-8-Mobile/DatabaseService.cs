using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyApp.Data
{
    public class DatabaseService
    {
        SQLiteAsyncConnection _db;

        public DatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
        }

        public Task InitializeAsync()
        {
            return _db.CreateTableAsync<Book>();
        }

        public Task<int> AddBookAsync(Book book)
            => _db.InsertAsync(book);

        public Task<List<Book>> GetBooksAfterAsync(string date)
            => _db.QueryAsync<Book>(
                "SELECT * FROM BookLibrary WHERE PublishDate > ?", date);

        public Task<List<Book>> GetFantasyWithCopiesAsync(int minCopies)
            => _db.QueryAsync<Book>(
                "SELECT * FROM BookLibrary WHERE Genre = ? AND CopiesAvailable > ?",
                "Fantasy", minCopies);
    }
}