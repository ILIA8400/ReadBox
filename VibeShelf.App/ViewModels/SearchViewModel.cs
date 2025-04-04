using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VibeShelf.App.Models;
using VibeShelf.App.Services;

namespace VibeShelf.App.ViewModels
{
    public partial class SearchViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        private string _searchText;
        private List<Book> _searchResults;
        private readonly IBookSearchService bookSearchService;
        private readonly FavoriteBookService favoriteBookService;

        // Observable collection to store favorite books
        private ObservableCollection<Book> _favoriteBooks = new ObservableCollection<Book>();
        public ObservableCollection<Book> FavoriteBooks
        {
            get => _favoriteBooks;
            set
            {
                _favoriteBooks = value;
                OnPropertyChanged();
            }
        }

        public SearchViewModel(IBookSearchService bookSearchService, FavoriteBookService favoriteBookService)
        {
            this.bookSearchService = bookSearchService;
            this.favoriteBookService = favoriteBookService;
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public List<Book> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                if (_isSearching != value)
                {
                    _isSearching = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNotSearching));
                }
            }
        }
        public bool IsNotSearching => !IsSearching;

        public ICommand SearchBooksCommand => new Command(async () => await SearchBooksAsync());

        // Command to add a book to favorite
        public ICommand AddToFavoriteCommand => new Command<Book>(async (book) => await AddToFavorite(book));

        private async Task SearchBooksAsync()
        {
            IsLoading = true;
            IsSearching = true;
            try
            {
                var books = await bookSearchService.SearchBooksAsync(_searchText);
                SearchResults = books;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsSearching = false;
            }
        }


        public async Task AddToFavorite(Book book)
        {
            if (book != null)
            {
                var existingBook = favoriteBookService.GetFavoriteBooks()
                                                      .FirstOrDefault(b => b.Name == book.Name && b.Author == book.Author);

                if (existingBook == null)
                {
                    favoriteBookService.AddFavoriteBook(new FavoriteBook
                    {
                        Name = book.Name,
                        Author = book.Author,
                        Thumbnail = book.Thumbnail
                    });
                    await AppShell.DisplayToastAsync("Book was Added");
                }
                else
                {
                    await AppShell.DisplaySnackbarAsync("Book Exist");
                }
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
