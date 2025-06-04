using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class ClaimIntimationImagePage : ContentPage
{
	public ClaimIntimationImagePage(ClaimAnimalViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}