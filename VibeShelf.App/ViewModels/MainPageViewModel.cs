using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

        private readonly IAudioManager _audioManager = AudioManager.Current;
        private IAudioPlayer _player;

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

        [ObservableProperty]
        private string playPauseIcon = "▶️";

        [RelayCommand]
        async Task TogglePodcast()
        {
            var file = await FileSystem.OpenAppPackageFileAsync("hideh.mp3");

            if (_player == null)
            {
                _player = _audioManager.CreatePlayer(file);
            }

            if (_player.IsPlaying)
            {
                _player.Pause();
                PlayPauseIcon = "▶️"; // دکمه پخش
            }
            else
            {
                _player.Play();
                PlayPauseIcon = "⏸️"; // دکمه پاز
            }
        }


    }
}
