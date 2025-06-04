using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.Service
{
    public class ClaimIntimationService : IClaimIntimationService
    {
        public async Task<List<IntimationCardModel>> GetClaimProposerDetails(string SearchLeadNo)
        {
            try
            {
                if (SearchLeadNo != null && SearchLeadNo != "")
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    var LeadNumber = SearchLeadNo;
                    HttpResponseMessage response = await client.GetAsync("Proposal/GetClaimProposerDetails/" + LeadNumber);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var result = JsonConvert.DeserializeObject<List<IntimationCardModel>>(jsonResponse);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        //ClaimAnimalData List
        public async Task<List<ClaimAnimalCard>> GetClaimAnimalCardList(string SelectPropId)
        {
            try
            {
                if (SelectPropId != null && SelectPropId != "")
                {

                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    HttpResponseMessage response = await client.GetAsync("Proposal/GetClaimAnimalCardList/" + SelectPropId);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        // EditProposalModel result = new EditProposalModel();
                        var result = JsonConvert.DeserializeObject<List<ClaimAnimalCard>>(jsonResponse);

                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        public async Task<ClaimIntimationBasic> GetBasicClaimIntimationByLeadId(string LeadNumber, string CattleNo)
        {
            try
            {
                if (LeadNumber != null && LeadNumber != "")
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    var paramiter = LeadNumber + "/" + CattleNo;
                    HttpResponseMessage response = await client.GetAsync("Proposal/GetCattleInspection/" + paramiter);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var result = JsonConvert.DeserializeObject<ClaimIntimationBasic>(jsonResponse);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        public async Task<ClaimIntimationResponceModel> SaveClaimIntimationDetailsOnServer(ClaimIntimationModel claimIntimation)
        {
            ClaimIntimationResponceModel ResponceModel = new ClaimIntimationResponceModel();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                //client.BaseAddress = new Uri("http://127.0.0.0:10852");
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(claimIntimation);//@"{""userName"" : ""RCAdmin"", ""password"" : ""R*iskcare""}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/Proposal/ClaimIntimationDetails", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ResponceModel = JsonConvert.DeserializeObject<ClaimIntimationResponceModel>(jsonResponse);

                  //  return ResponceModel;
                }
                return ResponceModel;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return ResponceModel;
            }
        }

        public async Task<string> SaveClaimIntimationImagesDetails(List<ClaimIntimationImageModel> Images)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(GlobalVariables.appUrl);
            //client.BaseAddress = new Uri("http://127.0.0.0:10852");


            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(Images);//@"{""userName"" : ""RCAdmin"", ""password"" : ""R*iskcare""}";

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/Proposal/SaveClaimIntimationImages", content);

            // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
            var result = await response.Content.ReadAsStringAsync();
            //var details = JObject.Parse(result);


            //dynamic json = JsonConvert.DeserializeObject(result);
            //var a=json.PropId;
            return result;
        }

        public async Task<List<UploadClaimCardModel>> GetUpdateClaimIntimationCardList()
        {
            try
            {

              
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                var User = Preferences.Get("emaiId", "");
                HttpResponseMessage response = await client.GetAsync("Proposal/GetUploadClaimIntimationCardList/" + User);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<List<UploadClaimCardModel>>(jsonResponse);
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

        public async Task<bool> SaveClaimIntimationImagesOnServer(ObservableCollection<ClaimIntimationImageModel> ClaimImageDetails)
        {
            try
            {
                if (ClaimImageDetails != null && ClaimImageDetails.Count != 0)
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(ClaimImageDetails);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("Proposal/SaveClaimIntimationImagesList", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var result = JsonConvert.DeserializeObject<bool>(jsonResponse);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return false;
        }
    }

}
                    
    

