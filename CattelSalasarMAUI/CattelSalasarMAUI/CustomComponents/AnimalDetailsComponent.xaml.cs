namespace CattelSalasarMAUI.CustomComponents;

public partial class AnimalDetailsComponent : ContentView
{
	public AnimalDetailsComponent()
	{
		InitializeComponent();
	}

    private async void PremiumRate_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            
            var oldText = e.OldTextValue;
            var newText = e.NewTextValue;

            if (Convert.ToDouble(PremiumRate.Text) >= 100)
            {
                await App.Current.MainPage.DisplayAlert("Alert!", "PremiumRate should be less than 100", "ok");
                PremiumRate.Text = string.Empty;
                SumInsured.Text = string.Empty;
                PremiumAmount.Text = string.Empty;
            }

            if (PremiumRate.Text != null)
            {
               
                PremiumAmount.Text = ((Convert.ToDouble(SumInsured.Text) * Convert.ToDouble(PremiumRate.Text)) / 100.0).ToString();

                //Benifisari share
                int tempSumStatepercentage = Preferences.Get("FarmerPercentageValue1", 0);
                
                if (tempSumStatepercentage != 0)
                {
                    if (SumInsured.Text != null && PremiumAmount.Text != null)
                    {
                        double tempPremiumAmount = Convert.ToDouble(PremiumAmount.Text);
                       
                        FarmerPremiumPercentage.Text = Convert.ToString((tempPremiumAmount * tempSumStatepercentage) / 100);
                     
                    }
                }
                else
                {
                    FarmerPremiumPercentage.Text = "0";
                   // animalData.BeneficiaryContributionAmount = "";
                }
                //State Share
                int tempGovStatePercentage = Preferences.Get("GovPercentageValue1", 0);
               // int tempGovStatePercentage = Convert.ToInt32(Preferences.Get("GovPercentageValue1", ""));
                if (tempGovStatePercentage != 0)
                {
                    if (SumInsured.Text != null)
                    {
                        double tempPremiumAmount = Convert.ToDouble(PremiumAmount.Text);
                        //double tempPremiumAmount = 85;
                        GovPremiumPercentage.Text = Convert.ToString((tempPremiumAmount * tempGovStatePercentage) / 100);
                       // animalData.StateShareAmount = GovPremiumPercentage.Text;
                    }
                }
                else
                {
                    GovPremiumPercentage.Text = "0";
                   // animalData.StateShareAmount = GovPremiumPercentage.Text;
                }

                //Central Share
                int tempGovCentralPercentage = Preferences.Get("GovCentralValue1", 0);
                if (tempGovCentralPercentage != 0)
                {
                    if (SumInsured.Text != null)
                    {
                        double tempPremiumAmount = Convert.ToDouble(PremiumAmount.Text);
                        //double tempPremiumAmount = 85;
                        CentralSharePercentage.Text = Convert.ToString((tempPremiumAmount * tempGovCentralPercentage) / 100);
                        //animalData.CentralShareAmount = CentralSharePercentage.Text;
                    }
                }
                else
                {
                    CentralSharePercentage.Text = "0";
                   // animalData.CentralShareAmount = CentralSharePercentage.Text;
                }
                // totalGovt Share
                int totalGovtSharePercentage = Preferences.Get("TotalGovtShareValue1", 0);
                if (totalGovtSharePercentage != 0)
                {
                    if (SumInsured.Text != null)
                    {
                        double tempPremiumAmount = Convert.ToDouble(PremiumAmount.Text);
                        //double tempPremiumAmount = 85;
                        var TotalShare = Convert.ToString((tempPremiumAmount * totalGovtSharePercentage) / 100);
                       
                    }
                }
                else
                {
                    var TotalShare = "0";
                    
                }
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }


    }
}