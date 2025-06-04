using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.Service
{
    public class AddressService : IAddressService
    {
        public async Task<ObservableCollection<AddressModel>> GetCurrentLocation()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();
                //Preferences.Set("Latitude", location.Latitude);
                // Preferences.Set("Longitude", location.Longitude);
               
                // Preferences.Set("Compass", temp);

                if (location != null)
                {
                    var temp = Convert.ToString(location.Timestamp);
                    return await GetGeocodeReverseData(location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        private async Task<ObservableCollection<AddressModel>> GetGeocodeReverseData(double latitude, double longitude)
        {
            ObservableCollection<AddressModel> addressList = new ObservableCollection<AddressModel>();
            IEnumerable<Placemark> placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
            foreach (var item in placemarks)
            {
                var addressModel = new AddressModel
                {
                    AdminArea = item.AdminArea,
                    CountryCode = item.CountryCode,
                    CountryName = item.CountryName,
                    FeatureName = item.FeatureName,
                    Locality = item.Locality,
                    PostalCode = item.PostalCode,
                    SubAdminArea = item.SubAdminArea,
                    SubLocality = item.SubLocality,
                    SubThoroughfare = item.SubThoroughfare,
                    Thoroughfare = item.Thoroughfare,
                };
                addressList.Add(addressModel);
            }
            return addressList;
        }



    }
}
