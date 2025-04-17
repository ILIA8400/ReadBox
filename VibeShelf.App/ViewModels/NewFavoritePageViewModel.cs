using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
using System.IO;
using System;
using VibeShelf.App.Services;
using VibeShelf.App.Models;

namespace VibeShelf.App.ViewModels
{
    public partial class NewFavoritePageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly FavoriteBookService favoriteBookService;

        public NewFavoritePageViewModel(FavoriteBookService favoriteBookService)
        {
            this.favoriteBookService = favoriteBookService;
        }

        private FileResult photo;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string author;

        [ObservableProperty]
        private ImageSource coverImage = "placeholder.png";

        [ObservableProperty]
        private FavoriteBook bookToEdit;

        [ObservableProperty]
        private string pageTitle = "Add Book";

        [ObservableProperty]
        private string submitButtonText = "Add to Favorites";

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("BookToEdit", out var bookObj) && bookObj is FavoriteBook book)
            {
                BookToEdit = book;
                Title = book.Name;
                Author = book.Author;
                CoverImage = book.Thumbnail;

                PageTitle = "Edit Book";
                SubmitButtonText = "Update Book";
            }
            else
            {
                PageTitle = "Add Book";
                SubmitButtonText = "Add to Favorites";
            }
        }




        [RelayCommand]
        private async Task OpenCameraAsync()
        {
            try
            {
                photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    CoverImage = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await AppShell.DisplaySnackbarAsync($"Camera error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task OpenGalleryAsync()
        {
            try
            {
                photo = await MediaPicker.PickPhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    CoverImage = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                await AppShell.DisplaySnackbarAsync($"Gallery error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SubmitAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
            {
                await AppShell.DisplaySnackbarAsync("Please enter both title and author.");
                return;
            }

            string imagePath = BookToEdit != null && photo == null
                ? BookToEdit.Thumbnail
                : await SaveImageToDisk(photo);

            if (BookToEdit != null)
            {
                // حالت ویرایش
                BookToEdit.Name = Title;
                BookToEdit.Author = Author;
                BookToEdit.Thumbnail = imagePath;

                favoriteBookService.UpdateFavoriteBook(BookToEdit);

                await AppShell.DisplayToastAsync($"✅ {Title} updated!");
            }
            else
            {
                // حالت افزودن جدید
                favoriteBookService.AddFavoriteBook(new Models.FavoriteBook()
                {
                    Author = Author,
                    Name = Title,
                    Thumbnail = imagePath,
                });

                await AppShell.DisplayToastAsync($"✅ {Title} by {Author} added!");
            }

            // برگشت به صفحه قبل
            await Shell.Current.GoToAsync("..");
        }



        private async Task<string> SaveImageToDisk(FileResult photo)
        {
            if (photo == null)
                return null;

            var fileName = $"{Guid.NewGuid()}.jpg";
            var localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using var sourceStream = await photo.OpenReadAsync();
            using var destinationStream = File.OpenWrite(localPath);
            await sourceStream.CopyToAsync(destinationStream);

            return localPath;
        }

    }
}
