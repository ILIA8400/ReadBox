using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeShelf.App.Models;
using VibeShelf.App.Services;
using VibeShelf.App.Views;

namespace VibeShelf.App.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly FavoriteBookService favoriteBookService;

        public MainPageViewModel(FavoriteBookService favoriteBookService)
        {
            this.favoriteBookService = favoriteBookService;
        }

        [ObservableProperty]
        private ObservableCollection<FavoriteBook> _recentlyAddedBooks;

        [RelayCommand]
        public void Appearing()
        {
            var bookList = favoriteBookService.GetFavoriteBooks().OrderByDescending(x => x.Id).Take(3).ToList();
            RecentlyAddedBooks = new ObservableCollection<FavoriteBook>(bookList);
        }

        [RelayCommand]
        private async Task GoToSearchPage()
        {
            await Shell.Current.GoToAsync("//SearchOnlinePage");
        }

        [RelayCommand]
        private async Task GoToFavoritesPage()
        {
            await Shell.Current.GoToAsync("//FvoritePage");
        }

    }
}
