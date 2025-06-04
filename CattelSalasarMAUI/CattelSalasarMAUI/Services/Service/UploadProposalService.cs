using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.Service
{
    public class UploadProposalService : IUploadProposalService
    {
       
        public async Task<string> UpdateAnimalDetailsToServer(AnimalDataModel animalData)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(animalData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("Proposal/SaveCattleDetailsList", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<string>(jsonResponse);
                    //IEnumerable<ProposerDetailModel> result = JsonConvert.DeserializeObject<IEnumerable<ProposerDetailModel>>(await response.Content.ReadAsStringAsync());

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
        public async Task<string> SaveAnimalImagesDetails(IEnumerable<ProposalImages> Images)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(GlobalVariables.appUrl);
            //client.BaseAddress = new Uri("http://127.0.0.0:10852");

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(Images);//@"{""userName"" : ""RCAdmin"", ""password"" : ""R*iskcare""}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/Proposal/SaveProposalImages", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            //var details = JObject.Parse(result);


            //dynamic json = JsonConvert.DeserializeObject(result);
            //var a=json.PropId;
            return result;
        }
        public async Task<string> GetUpdateClaimIntimationCardList()
        {
            try
            {
                var User = "";
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(User);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("Proposal/GetUploadClaimIntimationCardList", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<string>(jsonResponse);
                    //IEnumerable<ProposerDetailModel> result = JsonConvert.DeserializeObject<IEnumerable<ProposerDetailModel>>(await response.Content.ReadAsStringAsync());

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    
    }
}
