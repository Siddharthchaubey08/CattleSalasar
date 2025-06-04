using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private string _currentLocation;
        public string CurrentLocation
        {
            get => _currentLocation;
            set
            {
                if (_currentLocation != value)
                {
                    _currentLocation = value;
                    OnPropertyChanged(nameof(CurrentLocation));
                }
            }
        }

        [ObservableProperty]
        private string _loginUserName;

        [ObservableProperty]
        private ObservableCollection<AddressModel> _addressList;

        private IAddressService _addressService { get; set; }

        public HomePageViewModel(IAddressService addressService)
        {
            _addressService = addressService;
            AddressList = new ObservableCollection<AddressModel>();
            LoginUserName = Preferences.Get("userName", "");
            _ = InitializeAsync();
        }
        // Call this method when the page appears
        private async Task InitializeAsync()
        {
            await CurrentLocationsAsync();
        }

        [RelayCommand]
        public async Task CurrentLocationsAsync()
        {
            try
            {
                var addresses = await _addressService.GetCurrentLocation();
                if (addresses == null || !addresses.Any())
                {
                    CurrentLocation = "Location not available";
                    return;
                }

                AddressList = new ObservableCollection<AddressModel>(addresses);
                string[] addressArray = AddressList.Select(x => x.FeatureName).ToArray();
                var address = addressArray.Skip(0);
                CurrentLocation = string.Join(", ", address).Replace("Unnamed Road,", "").Trim();
                Preferences.Set("UserCurrentLocation", CurrentLocation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CurrentLocationsAsync: {ex.Message}");
                // Optionally notify the user with a toast or dialog
                // await Toast.Make("Something went wrong while fetching location!", ToastDuration.Long, 16).Show();
            }
        }

        [RelayCommand]
        public async Task GoToCattleInsuranceAsync()
        {
            try
            {
                await Shell.Current.GoToAsync($"///createProposal");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation Error (Cattle Insurance): {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task GoToFisheryInsuranceAsync()
        {
            try
            {
                await Shell.Current.GoToAsync($"//homePage/fisheryInsurance");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation Error (Fishery Insurance): {ex.Message}");
            }
        }
    }
}

