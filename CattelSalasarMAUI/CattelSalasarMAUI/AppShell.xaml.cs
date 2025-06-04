using CattelSalasarMAUI.Views;
using CattelSalasarMAUI.Views.ClaimIntimation;
using CattelSalasarMAUI.Views.ProposalDetails;
using System.Windows.Input;

namespace CattelSalasarMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var test = Preferences.Get("Remember", false);
            if (Preferences.Get("Remember", false))
            {
                myShell.CurrentItem = homePage;
            }
            else
            {
                // myShell.CurrentItem = loginPage;
                Application.Current.MainPage = new LoginPage();
            }
            
            this.BindingContext = this;
            Routing.RegisterRoute("homePage", typeof(HomePage));
            //Routing.RegisterRoute("registrationPage", typeof(RegistrationPage));
            //Routing.RegisterRoute("createProposal", typeof(CreateProposal));
            // Routing.RegisterRoute("editPreviewPage", typeof(EditPreviewPage));
            // Routing.RegisterRoute("editPreviewPage/editProposalDetails", typeof(EditProposalDetails));
            //Routing.RegisterRoute("editPreviewPage/editProposalDetails/editAnimalCardPage", typeof(EditAnimalCardPage));
            //Routing.RegisterRoute("uploadPreviewPage", typeof(UploadPreviewPage));
            //Routing.RegisterRoute("uploadAnimalDetailsPage", typeof(UploadAnimalDetailsPage));
            // Routing.RegisterRoute("cardClaimIntimation", typeof(CardClaimIntimation));
            Routing.RegisterRoute("uploadClaimIntimationCard", typeof(UploadClaimIntimationCard));

            Routing.RegisterRoute("animalDetails", typeof(ProposalAnimalDetails));
           Routing.RegisterRoute("editProposalDetails", typeof(EditProposalDetails));
           Routing.RegisterRoute("editAnimalCardPage", typeof(EditAnimalCardPage));
           Routing.RegisterRoute("editAnimalDetailsPage", typeof(EditAnimalDetailsPage));
           Routing.RegisterRoute("uploadAnimalCardPage/uploadAnimalDetailsPage", typeof(UploadAnimalDetailsPage));
           Routing.RegisterRoute("uploadAnimalCardPage", typeof(UploadAnimalCardPage));
           Routing.RegisterRoute("claimIntimationPage", typeof(ClaimIntimationPage));
           Routing.RegisterRoute("claimAnimalCardPage", typeof(ClaimAnimalCardPage));
           Routing.RegisterRoute("claimIntimationImagePage", typeof(ClaimIntimationImagePage));
           Routing.RegisterRoute("uploadClaimIntimation", typeof(UploadClaimIntimation));
        }

        public ICommand ExecuteLogout => new Command(async () =>
        {
            
            Preferences.Remove("Remember");
            Application.Current.MainPage = new LoginPage();
        });
    }
}
