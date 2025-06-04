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
    public class CreateProposalService : ICreateProposalService
    {
        public async Task<CreateProposalResult> SaveBasicDetails(ProposalBasicDetailModel ProposalBasicDetail)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(GlobalVariables.appUrl);
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(ProposalBasicDetail);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("Proposal/EiditProposerDetails", content);
                if (response.IsSuccessStatusCode)
                {
                    
                    var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    
                    var result = JsonConvert.DeserializeObject<CreateProposalResult>(jsonResponse);
                    
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
