using VibeShelf.App.ViewModels;
using VibeShelf.App.Views;

namespace VibeShelf.App.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            BindingContext = mainPageViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MainPageViewModel viewModel)
            {
                viewModel.AppearingCommand.Execute(null);
            }
        }

    }

}
