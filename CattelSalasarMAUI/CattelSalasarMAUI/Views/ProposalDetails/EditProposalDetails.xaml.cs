using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ProposalDetails;

public partial class EditProposalDetails : ContentPage
{
	public EditProposalDetails(ProposalDetailsViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}