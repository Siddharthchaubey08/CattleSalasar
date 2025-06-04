using CattelSalasarMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface IAddressService
    {
        Task<ObservableCollection<AddressModel>> GetCurrentLocation();
    }
}
