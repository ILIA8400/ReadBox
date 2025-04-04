using VibeShelf.App.ViewModels;

namespace VibeShelf.App.Views;

public partial class SearchOnlinePage : ContentPage
{
    public SearchOnlinePage(SearchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}