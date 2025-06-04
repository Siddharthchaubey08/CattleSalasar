using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageViewModel();

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