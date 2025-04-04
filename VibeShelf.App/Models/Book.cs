using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeShelf.App.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPages { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public string Mood { get; set; }
        public string Thumbnail { get; set; }

    }
}
