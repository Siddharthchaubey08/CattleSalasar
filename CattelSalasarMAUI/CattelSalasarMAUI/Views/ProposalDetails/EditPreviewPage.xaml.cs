using CattelSalasarMAUI.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Handlers;

namespace CattelSalasarMAUI.Views.ProposalDetails;
public partial class EditPreviewPage : ContentPage
{
	public EditPreviewPage(EditProposalViewModel viewModel)
	{
		InitializeComponent();
		ProposalDate.Date= DateTime.Now;
		//ProposalDate.MaximumDate = DateTime.Now.AddDays(2);
        ProposalDate.Unfocus();
        CalendarEntryBox.Text = string.Empty;
        this.BindingContext = viewModel;

	}
   
    
    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        //var viewModel = BindingContext as EditProposalViewModel;
        //if (ProposalDate.Date == e.NewDate)
        //{
        //    ProposalDate.Date = DateTime.Now.AddDays(-1); // Temporarily set to yesterday
        //    ProposalDate.Date = e.NewDate; // Set the selected date again
        //    if (viewModel != null)
        //    {
        //        viewModel.SelectedDate = e.NewDate.ToString("dd-MMM-yyyy");
        //        viewModel.IsCalendarVisible = true;
        //    }
        //}

        // Update the selected date in the ViewModel
        var viewModel = BindingContext as EditProposalViewModel;
        if (viewModel != null)
        {
            viewModel.SelectedDate = e.NewDate.ToString("dd-MMM-yyyy");
            viewModel.IsCalendarVisible = true;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        #if ANDROID
            // Safely cast to IDatePickerHandler for Android-specific behavior
            if (ProposalDate.Handler is IDatePickerHandler handler)
            {
                handler.PlatformView?.PerformClick(); // Opens the DatePicker dialog on Android
            }

        #endif
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