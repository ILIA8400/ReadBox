using VibeShelf.App.ViewModels;

namespace VibeShelf.App.Views;

public partial class NewFavoritePage : ContentPage
{
    public NewFavoritePage(NewFavoritePageViewModel newFavoritePageViewModel)
	{
		BindingContext = newFavoritePageViewModel;
        InitializeComponent();
	}
}