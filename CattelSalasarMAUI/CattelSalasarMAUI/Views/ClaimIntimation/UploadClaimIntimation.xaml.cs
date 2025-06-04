using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ClaimIntimation;

public partial class UploadClaimIntimation : ContentPage
{
	public UploadClaimIntimation(UploadClaimIntimationViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;

    }
}