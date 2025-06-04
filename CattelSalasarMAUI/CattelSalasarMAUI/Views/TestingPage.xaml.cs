using CattelSalasarMAUI.ViewModels;

namespace CattelSalasarMAUI.Views;

public partial class TestingPage : ContentPage
{
	public TestingPage(ProposalAnimalDetailsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}