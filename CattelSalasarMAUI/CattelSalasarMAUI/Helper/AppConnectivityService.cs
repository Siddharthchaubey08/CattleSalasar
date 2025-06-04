using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Helper
{
    public class AppConnectivityService
    {
        public AppConnectivityService()
        {
            // Subscribe to network changes
            Connectivity.ConnectivityChanged += Connectivity_Changed;
            Connectivity.ConnectivityChanged += async (sender, e) => await FetchLocationAsync();
        }
        private void Connectivity_Changed(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("No Internet", "You have lost your internet connection!", "OK");
                });
            }
        }

        private async Task FetchLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
                }

                if (location != null)
                {
                    // Update location in UI (Example: Update Label)
                    await Application.Current.MainPage.DisplayAlert("Access Location!", "", $"Latitute: {location.Latitude}, Longitute: {location.Longitude}", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Location Error", "Unable to fetch location after login!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
            }
        }
    }
}
