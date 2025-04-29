using MyApp.Data;
using SQLite;
using System.Collections.ObjectModel;

namespace Lab_8_Mobile
{
    // Модель для підрахунку жанрів
    public class GenreCount
    {
        public string Genre { get; set; }
        public int Count { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        // Зібрані дані
        public ObservableCollection<Book> Books { get; } = new();
        public ObservableCollection<GenreCount> GenreCounts { get; } = new();

        // Видимість секцій
        public bool ShowBooks { get; set; } = true;
        public bool ShowCounts { get; set; } = false;

        SQLiteAsyncConnection _db;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            // Шлях і ініціалізація бази
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "library.db3");

            // якщо потрібно скинути базу перед роботою
            //if (File.Exists(dbPath))
            //    File.Delete(dbPath);

            _db = new SQLiteAsyncConnection(dbPath);
            _ = InitializeAndLoadAll();
        }

        // Загальне завантаження (створити таблицю, вставити демо і показати всі)
        async Task InitializeAndLoadAll()
        {
            await _db.CreateTableAsync<Book>();
            var count = await _db.Table<Book>().CountAsync();
            if (count == 0)
            {
                await _db.InsertAllAsync(new[]
                {
            new Book { ISBN="978-0140449136", Title="The Odyssey",                   Author="Homer",             PublishDate="1996-11-01", Genre="Epic",        CopiesAvailable=3 },
            new Book { ISBN="978-0261103573", Title="The Fellowship of the Ring",   Author="Tolkien, J.R.R.",   PublishDate="1954-07-29", Genre="Fantasy",     CopiesAvailable=5 },
            new Book { ISBN="978-0307277671", Title="The Road",                      Author="McCarthy, C.",      PublishDate="2006-09-26", Genre="Post-apoc.",  CopiesAvailable=2 },
            new Book { ISBN="978-0061120084", Title="To Kill a Mockingbird",         Author="Lee, H.",           PublishDate="1960-07-11", Genre="Fiction",     CopiesAvailable=4 },
            new Book { ISBN="978-0201633610", Title="Design Patterns",               Author="Gamma et al.",      PublishDate="1994-10-31", Genre="Technical",   CopiesAvailable=10 },
            new Book { ISBN="978-0756404741", Title="The Name of the Wind",          Author="Rothfuss, P.",      PublishDate="2007-03-27", Genre="Fantasy",     CopiesAvailable=6 },
            new Book { ISBN="978-0553103540", Title="A Game of Thrones",             Author="Martin, G.R.R.",    PublishDate="1996-08-06", Genre="Fantasy",     CopiesAvailable=7 },
            new Book { ISBN="978-0451524935", Title="1984",                          Author="Orwell, G.",        PublishDate="1949-06-08", Genre="Dystopian",   CopiesAvailable=5 },
            new Book { ISBN="978-0804139021", Title="The Martian",                   Author="Weir, A.",          PublishDate="2014-02-11", Genre="Sci-fi",      CopiesAvailable=8 },
            new Book { ISBN="978-0132350884", Title="Clean Code",                    Author="Martin, R.C.",      PublishDate="2008-08-11", Genre="Technical",   CopiesAvailable=12 },
            new Book { ISBN="978-0307454546", Title="The Girl with the Dragon Tattoo",Author="Larsson, S.",       PublishDate="2005-08-01", Genre="Mystery",     CopiesAvailable=4 },
            new Book { ISBN="978-0062316097", Title="Sapiens: A Brief History of Humankind",Author="Harari, Y.",  PublishDate="2011-02-04", Genre="Non-Fiction", CopiesAvailable=9 },
            new Book { ISBN="978-0060850524", Title="Brave New World",               Author="Huxley, A.",        PublishDate="1932-08-30", Genre="Dystopian",   CopiesAvailable=3 },
            new Book { ISBN="978-0812550702", Title="Ender's Game",                  Author="Card, O.S.",        PublishDate="1985-01-15", Genre="Sci-fi",      CopiesAvailable=6 },
            new Book { ISBN="978-0307887443", Title="Ready Player One",             Author="Cline, E.",         PublishDate="2011-08-16", Genre="Sci-fi",      CopiesAvailable=10 }
        });
            }
            await ExecuteShowAll();
        }


        // Показати всі
        async void OnShowAll(object s, EventArgs e) => await ExecuteShowAll();

        async Task ExecuteShowAll()
        {
            var list = await _db.Table<Book>().ToListAsync();
            PopulateBooks(list);
            ShowBooks  = true;
            ShowCounts = false;
            OnPropertyChanged(nameof(ShowBooks));
            OnPropertyChanged(nameof(ShowCounts));
        }

        // Показати після 2000
        async void OnAfter2000(object s, EventArgs e)
        {
            var list = await _db.QueryAsync<Book>(
                "SELECT * FROM BookLibrary WHERE PublishDate > ?", "2000-01-01");
            PopulateBooks(list);
            ShowBooks  = true;
            ShowCounts = false;
            OnPropertyChanged(nameof(ShowBooks));
            OnPropertyChanged(nameof(ShowCounts));
        }

        // Показати лише Fantasy із >3 копіями
        async void OnFantasy(object s, EventArgs e)
        {
            var list = await _db.QueryAsync<Book>(
                "SELECT * FROM BookLibrary WHERE Genre = ? AND CopiesAvailable > ?",
                "Fantasy", 3);
            PopulateBooks(list);
            ShowBooks  = true;
            ShowCounts = false;
            OnPropertyChanged(nameof(ShowBooks));
            OnPropertyChanged(nameof(ShowCounts));
        }

        // Показати підрахунок за жанром
        async void OnCountPerGenre(object s, EventArgs e)
        {
            var result = await _db.QueryAsync<GenreCount>(
                "SELECT Genre AS Genre, COUNT(*) AS Count FROM BookLibrary GROUP BY Genre");
            GenreCounts.Clear();
            foreach (var g in result)
                GenreCounts.Add(g);

            ShowBooks  = false;
            ShowCounts = true;
            OnPropertyChanged(nameof(ShowBooks));
            OnPropertyChanged(nameof(ShowCounts));
        }

        void PopulateBooks(System.Collections.Generic.List<Book> list)
        {
            Books.Clear();
            foreach (var b in list)
                Books.Add(b);
        }
    }
}