using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            DisplayAlert("No Internet", "You are currently offline!", "OK");
        }
        
    }
}