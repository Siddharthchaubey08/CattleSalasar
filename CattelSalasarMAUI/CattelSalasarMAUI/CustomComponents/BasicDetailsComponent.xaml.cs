using static SQLite.SQLite3;

namespace CattelSalasarMAUI.CustomComponents;

public partial class BasicDetailsComponent : ContentView
{
	public BasicDetailsComponent()
	{
		InitializeComponent();
        PaymentModeSection.IsVisible=false;
        SurveyDate.MaximumDate = DateTime.Today;
        CustomerDOB.MaximumDate = DateTime.Today;
        
    }
    private void PaymentReceived_SelectedIndexChanged(object sender, EventArgs e)
    {
        var value = PaymentReceived.SelectedItem;
        if (value == "Yes")
        {
            PaymentModeSection.IsVisible = true;
        }
        else
        {
            PaymentModeSection.IsVisible = false;
        }
    }

    private void CustomerDOB_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (CustomerDOB != null)
        {
            CustomerDOB.MaximumDate = DateTime.Today;

            // Assuming CustomerDOB.Date is nullable
            var birthDate = CustomerDOB.Date != null ? CustomerDOB.Date : DateTime.Now;

            CalculateAge(birthDate);
        }
        else
        {
            Console.WriteLine("CustomerDOB is null.");
            // Optionally initialize it here
            // CustomerDOB = new YourDatePickerType();
        }


    }

    //Object reference not set to an instance of an object
    public int CalculateAge(DateTime birthDate)
    {
        DateTime currentDate = DateTime.Today;
        int age = currentDate.Year - birthDate.Year;
        // Check if the birth date has already occurred this year
        if (birthDate > currentDate.AddYears(-age))
            age--;
        CustomerAge.Text = age + " " + "Years";
        return age;
    }
}