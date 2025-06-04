using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using Newtonsoft.Json;
using System.Text;

namespace CattelSalasarMAUI.Services.Service
{
    public class EditProposalService : IEditProposalService
    {
        public async Task<List<ProposalBasicDetailModel>> GetAllProposalMethod(string UserId, string SelectDate)
        {
            try
            {
                if (SelectDate != null && SelectDate != "")
                {
                    List<ProposalBasicDetailModel> result = null;
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    var proposerData = UserId + "/" + SelectDate;
                    HttpResponseMessage response = await client.GetAsync("Proposal/GetProposalListByUserid/" + proposerData);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        result = JsonConvert.DeserializeObject<List<ProposalBasicDetailModel>>(jsonResponse).ToList();
                       
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

        public async Task<List<ProposalBasicDetailModel>> GetUploadDataProposalListMethod(string UserId)
        {
            try
            {
                List<ProposalBasicDetailModel> result1 = null;
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                var proposerData = UserId;
                HttpResponseMessage response = await client.GetAsync("Proposal/GetBasicDetailsList/" + proposerData);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result1 = JsonConvert.DeserializeObject<List<ProposalBasicDetailModel>>(jsonResponse).ToList();

                    return result1;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }

        //Edit Get Basic_Details & AnimalCard
        public async Task<EditProposalModel> GetEditProposalList(string SelectPropId)
        {
            try
            {
                if (SelectPropId != null && SelectPropId != "")
                {

                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    var proposerData = SelectPropId;
                    //string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(proposerData);
                    // var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.GetAsync("Proposal/GetProposalDetailsList/" + proposerData);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                       // EditProposalModel result = new EditProposalModel();
                        var result = JsonConvert.DeserializeObject<EditProposalModel>(jsonResponse);

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

        //Save Edit Basic Details
        public async Task<CreateProposalResult> SaveEditProposalBasicDetails(ProposalBasicDetailModel basicDetailModel1)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(basicDetailModel1);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("Proposal/EiditProposerDetails", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = JsonConvert.DeserializeObject<CreateProposalResult>(jsonResponse);
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

        //Save Edit Animal Details
        public async Task<string> EditAnimalDetailsSave(AnimalDataModel editanimalData)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(editanimalData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("Proposal/EidtCattleDetails", content);

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
//"Unexpected character encountered while parsing value: {. Path 'animalData', line 1, position 1372."