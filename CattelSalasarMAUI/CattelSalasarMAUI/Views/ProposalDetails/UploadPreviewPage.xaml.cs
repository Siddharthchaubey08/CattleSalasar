using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ProposalDetails;

public partial class UploadPreviewPage : ContentPage
{
	public UploadPreviewPage(UploadProposalViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UploadProposalViewModel viewModel)
        {
            viewModel.GetUploadCardListMethod();
        }

        // Internet connection Services
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            DisplayAlert("No Internet", "You are currently offline!", "OK");
        }
    }
   
}