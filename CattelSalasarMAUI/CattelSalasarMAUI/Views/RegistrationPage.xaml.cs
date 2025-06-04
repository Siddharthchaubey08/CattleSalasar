using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
		this.BindingContext = new UserRegistrationViewMode();
	}
}