using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class UploadClaimIntimationCard : ContentPage
{
	public UploadClaimIntimationCard(UploadClaimIntimationViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}