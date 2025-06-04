using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class CardClaimIntimation : ContentPage
{
	public CardClaimIntimation(CardClaimIntimationViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}