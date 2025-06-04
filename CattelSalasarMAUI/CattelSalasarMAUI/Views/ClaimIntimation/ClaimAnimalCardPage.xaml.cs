using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class ClaimAnimalCardPage : ContentPage
{
	public ClaimAnimalCardPage(ClaimAnimalViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}