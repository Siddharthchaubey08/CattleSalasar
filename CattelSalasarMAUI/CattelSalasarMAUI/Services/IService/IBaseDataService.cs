
using CattelSalasarMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface IBaseDataService
    {
        Task<ObservableCollection<BoState>> GetState();
        Task<ObservableCollection<BoDistrict>> GetDistric(string StateCode);
        Task<ObservableCollection<BoTehsil>> GetBlock(string DistrictName);
        Task<ObservableCollection<GetAllSchemeModel>> GetAllSchemeData();
        Task<List<GetStateWiseSchemesModel>> GetStateWiseSchemaDataList(string stateCode, string schemeId);
    }
}
