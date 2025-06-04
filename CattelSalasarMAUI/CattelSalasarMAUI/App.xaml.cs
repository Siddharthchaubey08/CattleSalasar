using CattelSalasarMAUI.Views;

namespace CattelSalasarMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           
            // Register Global Connectivity Listener
            _ = new Helper.AppConnectivityService();

            var compair = (Preferences.Get("Remember", false));
            if (compair)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
    }
}
