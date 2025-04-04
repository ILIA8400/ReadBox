using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeShelf.App.Models;

namespace VibeShelf.App.Services
{
    public class BookService
    {
        private readonly SQLiteConnection _database;

        public BookService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);            
            _database.CreateTable<Book>();    
        }

        public int AddBook(Book book) => _database.Insert(book);

        public int DeleteBook(int id) => _database.Delete<Book>(id);

        public List<Book> GetBooks() => _database.Table<Book>().ToList();

        public int UpdateBook(Book book) => _database.Update(book);

    }
}
