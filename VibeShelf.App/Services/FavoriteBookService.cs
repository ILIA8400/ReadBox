using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeShelf.App.Models;

namespace VibeShelf.App.Services
{
    public class FavoriteBookService
    {
        private readonly SQLiteConnection _database;

        public FavoriteBookService(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<FavoriteBook>();
        }

        // اضافه کردن کتاب به لیست علاقه‌مندی‌ها
        public int AddFavoriteBook(FavoriteBook book) => _database.Insert(book);

        // اضافه کردن کتاب به لیست علاقه‌مندی‌ها بر اساس نام (اگر وجود نداشته باشد)
        public int AddFavoriteBookByName(string name, string author, string thumbnail)
        {
            // ابتدا بررسی می‌کنیم که آیا کتاب با این نام در دیتابیس موجود است یا نه
            var existingBook = _database.Table<FavoriteBook>().FirstOrDefault(b => b.Name == name);
            if (existingBook == null) // اگر کتاب وجود ندارد
            {
                var newBook = new FavoriteBook
                {
                    Name = name,
                    Author = author,
                    Thumbnail = thumbnail
                };
                return _database.Insert(newBook); // اضافه کردن کتاب جدید
            }
            return 0; // کتاب قبلاً وجود دارد و نیازی به اضافه کردن نیست
        }

        // حذف کتاب از لیست علاقه‌مندی‌ها بر اساس Id
        public int RemoveFavoriteBook(int id) => _database.Delete<FavoriteBook>(id);

        // دریافت لیست کتاب‌های مورد علاقه
        public List<FavoriteBook> GetFavoriteBooks() => _database.Table<FavoriteBook>().ToList();

        // آپدیت اطلاعات یک کتاب در لیست علاقه‌مندی‌ها
        public int UpdateFavoriteBook(FavoriteBook book) => _database.Update(book);
    }
}

