using Microsoft.Maui.Controls.Internals;
using VibeShelf.App.ViewModels;

namespace VibeShelf.App.Views;

public partial class FvoritePage : ContentPage
{
	public FvoritePage(FavoriteViewModel favoriteViewModel)
	{
		BindingContext = favoriteViewModel;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is FavoriteViewModel viewModel)
        {
            viewModel.AppearingCommand.Execute(null);
        }
    }

}