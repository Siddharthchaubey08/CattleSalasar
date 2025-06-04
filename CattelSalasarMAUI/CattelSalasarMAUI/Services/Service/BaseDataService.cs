using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.Service
{
    public class BaseDataService : IBaseDataService
    {
        
        public async Task<ObservableCollection<BoState>> GetState()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.appUrl);
            HttpResponseMessage response = await client.GetAsync("api/Location/GetAllStates");
            //var result = await response.Content.ReadAsStringAsync();
            ObservableCollection<BoState> json = JsonConvert.DeserializeObject<ObservableCollection<BoState>>(await response.Content.ReadAsStringAsync());

            return json;
        }

        public async Task<ObservableCollection<BoDistrict>> GetDistric(string StateCode)
        {
           
            var client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.appUrl);

            HttpResponseMessage response = await client.GetAsync("api/Location/GetDistricts/" + StateCode);
            var result = await response.Content.ReadAsStringAsync();
            ObservableCollection<BoDistrict> json1 = JsonConvert.DeserializeObject<ObservableCollection<BoDistrict>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            return json1;
        }

        public async Task<ObservableCollection<BoTehsil>> GetBlock(string DistrictName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.appUrl);

            HttpResponseMessage response = await client.GetAsync("api/Location/GetTehsils/" + DistrictName);
            var result = await response.Content.ReadAsStringAsync();
            ObservableCollection<BoTehsil> json = JsonConvert.DeserializeObject<ObservableCollection<BoTehsil>>(await response.Content.ReadAsStringAsync().ConfigureAwait(true));

            return json;
        }

        public async Task<ObservableCollection<GetAllSchemeModel>> GetAllSchemeData()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.appUrl);
            HttpResponseMessage response = await client.GetAsync("api/Location/GetAllScheme");
            //var result = await response.Content.ReadAsStringAsync();
            ObservableCollection<GetAllSchemeModel> json = JsonConvert.DeserializeObject<ObservableCollection<GetAllSchemeModel>>(await response.Content.ReadAsStringAsync());

            return json;
        }

        public async Task<List<GetStateWiseSchemesModel>> GetStateWiseSchemaDataList(string stateCode, string schemeId)
        {
            try
            {
                //GetStateWiseSchemesModel data = new GetStateWiseSchemesModel();
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                var InputParamiter = stateCode + "/" + schemeId;
                HttpResponseMessage response = await client.GetAsync("api/Location/GetStateWiseSchemes/" + InputParamiter);
                if (response != null)
                {
                    var result = await response.Content.ReadAsStringAsync();
                     var data = JsonConvert.DeserializeObject<List<GetStateWiseSchemesModel>>(result);

                    return data;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            return null;
        }
    }
}
