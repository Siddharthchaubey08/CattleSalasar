using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.Services.Service;
using CattelSalasarMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _userName;

        [ObservableProperty]
        string _password;

        [ObservableProperty]
        bool _isRemember;
        [ObservableProperty]
        bool _loginLoder;

        //private ILoginPageService _loginPageService;
        LoginPageService _loginPageService = new LoginPageService();
        public LoginPageViewModel()
        {
              
        }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                if (GlobalVariables.IsOnline() == true)
                {
                    //var NewImageName = DateTime.Now.ToString("dd:MM:yyy:hh:ms").Replace(":", "") + ".jpg";
                    //await DisplayAlert("Alert", NewImageName, "OK");

                    if ((string.IsNullOrWhiteSpace(UserName)) || (string.IsNullOrWhiteSpace(Password)) || (string.IsNullOrEmpty(UserName)) || (string.IsNullOrEmpty(Password)))
                    {
                        await App.Current.MainPage.DisplayAlert("Enter Data", "Enter emailid and password", "OK");
                        return;
                    }
                    else if (UserName.Trim() != null && Password.Trim() != null)
                    {
                        try
                        {
                            LoginLoder = true;
                            AuthenticateUser user = new AuthenticateUser()
                            { 
                                UserName = UserName.Trim(),
                                Password = Password.Trim(),
                                ImeiNo = GlobalVariables.IMEI_No,
                                RememberMe = IsRemember,
                            };

                            var data =await _loginPageService.GetUserAuthorization(user);
                            if (data != null)
                            {
                                LoginLoder = false;
                                Preferences.Set("userName", data.UserName);
                                Preferences.Set("emaiId", data.Email);
                                Preferences.Set("mobileNo", data.Mobile);
                                Preferences.Set("Remember", IsRemember);
                                Application.Current.MainPage = new AppShell();
                            }
                            Preferences.Set("UserId", UserName);
                        }  
                        catch (Exception ex)
                        {
                            await App.Current.MainPage.DisplayAlert("ElseIf Inner", "Catch mode" + ex.Message.ToString(), "OK");
                            ex.Message.ToString();
                        }
                        
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("OffLine", "Newtwork not working", "OK");
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw;
            }

        }
         [RelayCommand]
        public async Task ExecuteRegistration()
        {
            try
            {
                try
                {
                    //Application.Current.MainPage = new NavigationPage(new RegistrationPage());

                    // Application.Current.MainPage =new NavigationPage(new RegistrationPage());
                    Application.Current.MainPage =new RegistrationPage();

                    //await Shell.Current.GoToAsync($"///loginPage/registrationPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Navigation Error (Cattle Insurance): {ex.Message}");
                }

                //await App.Current.MainPage.DisplayAlert("OffLine", "Newtwork not working", "OK");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw;
            }

        }

    }
}
