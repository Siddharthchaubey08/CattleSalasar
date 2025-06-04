using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class ClaimIntimationPage : ContentPage
{
	public ClaimIntimationPage(ClaimIntimationViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}