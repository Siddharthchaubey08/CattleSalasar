using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views.ProposalDetails;

public partial class ProposalAnimalDetails : ContentPage
{
	public ProposalAnimalDetails(ProposalAnimalDetailsViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}