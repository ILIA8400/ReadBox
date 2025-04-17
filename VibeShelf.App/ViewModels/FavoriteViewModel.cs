using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VibeShelf.App.Models;
using VibeShelf.App.Services;
using VibeShelf.App.Views;

namespace VibeShelf.App.ViewModels
{
    public partial class FavoriteViewModel : ObservableObject
    {
        private readonly FavoriteBookService favoriteBookService;

        [ObservableProperty]
        private ObservableCollection<FavoriteBook> favoriteBooks = new();

        public FavoriteViewModel(FavoriteBookService favoriteBookService)
        {
            this.favoriteBookService = favoriteBookService;
        }

        [RelayCommand]
        public void Appearing()
        {
            var bookList = favoriteBookService.GetFavoriteBooks();
            FavoriteBooks = new ObservableCollection<FavoriteBook>(bookList); 
        }

        [RelayCommand]
        private async Task RemoveFavoriteBook(FavoriteBook book)
        {
            favoriteBookService.RemoveFavoriteBook(book.Id);
            favoriteBooks.Remove(favoriteBooks.FirstOrDefault(x => x.Id == book.Id));
            
            await AppShell.DisplayToastAsync("Item was Remove");
        }

        [RelayCommand]
        public async Task New()
        {

            await Shell.Current.GoToAsync(nameof(NewFavoritePage),true);
        }

        [RelayCommand]
        private async Task EditFavoriteBook(FavoriteBook book)
        {
            if (book == null)
                return;

            // می‌فرستیمش به صفحه افزودن، ولی با اطلاعات اولیه
            var navigationParameter = new Dictionary<string, object>
            {
                { "BookToEdit", book }
            };

            await Shell.Current.GoToAsync(nameof(NewFavoritePage), navigationParameter);
        }

    }
}
