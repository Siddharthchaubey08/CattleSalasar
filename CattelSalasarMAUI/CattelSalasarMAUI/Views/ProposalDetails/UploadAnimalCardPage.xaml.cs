using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ProposalDetails;

public partial class UploadAnimalCardPage : ContentPage
{
	public UploadAnimalCardPage(ProposalAnimalDetailsViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}