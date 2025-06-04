using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.Service
{
    public class LoginPageService : ILoginPageService
    {
        public async Task<AuthUserModel> GetUserAuthorization(AuthenticateUser user)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("/api/UserManager/authenticate", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var details = JsonConvert.DeserializeObject<AuthUserModel>(result);
                        return details;
                    }
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
