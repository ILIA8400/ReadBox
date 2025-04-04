using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeShelf.App.Models;

namespace VibeShelf.App.Services
{
    public interface IBookSearchService
    {
        Task<List<Book>> SearchBooksAsync(string title);
    }
}
