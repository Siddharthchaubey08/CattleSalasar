using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ProposalDetails;

public partial class CreateProposal : ContentPage
{
	public CreateProposal(ProposalDetailsViewModel viewModel)
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