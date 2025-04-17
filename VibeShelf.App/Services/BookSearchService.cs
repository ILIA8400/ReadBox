using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeShelf.App.Models;

namespace VibeShelf.App.Services
{
    public class BookSearchService : IBookSearchService
    {
        private const string ApiUrl = "https://openlibrary.org/search.json?q=";

        public async Task<List<Book>> SearchBooksAsync(string title)
        {
            var books = new List<Book>();

            try
            {
                using var client = new HttpClient();
                var response = await client.GetStringAsync(ApiUrl + title);

                var json = JObject.Parse(response);
                var docs = json["docs"];

                if (docs != null)
                {
                    foreach (var doc in docs)
                    {
                        var book = new Book
                        {
                            Name = doc["title"]?.ToString(),
                            Author = doc["author_name"]?[0]?.ToString(),
                            Thumbnail = doc["cover_i"] != null ? $"https://covers.openlibrary.org/b/id/{doc["cover_i"]}-M.jpg" : null,
                            Description = doc["description"]?.ToString() ?? "No description available",
                            Price = 0 // Open Library قیمت رو نمی‌ده، ممکنه اینجا نیاز به تغییر داشته باشه
                        };

                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
            }

            return books.Take(50).ToList();
        }
    }

}
