using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.Services.Service;
using CattelSalasarMAUI.SQLiteDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Media.Abstractions;
using Plugin.Media;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using CattelSalasarMAUI.Views;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class UserRegistrationViewMode : BaseViewModel
    {
        [ObservableProperty]
        string _userName;

        [ObservableProperty]
        string _password;

        [ObservableProperty]
        string _confirmPassword;

        [ObservableProperty]
        string _phoneNumber;

        //[ObservableProperty]
        //string _stateName;

        //[ObservableProperty]
        //string _districtName;

        [ObservableProperty]
        string _emailID;

        [ObservableProperty]
        string _adharNumber;
        
        [ObservableProperty]
        string _panCardNo;

        [ObservableProperty]
        string _nameOfBank;

        [ObservableProperty]
        string _bankAccountNo;

        [ObservableProperty]
        string _iFSCNumber;

        private BoState _selectedState;
        public BoState SelectedState
        {
            get => _selectedState;
            set
            {
                if (_selectedState != value)
                {
                    _selectedState = value;
                    OnPropertyChanged(nameof(SelectedState));
                    District(SelectedState.StateCode);
                }
            }
        }

        private ImageSource _selfieeImage;
        public ImageSource SelfieeImage
        {
            get => _selfieeImage;
            set
            {
                if (_selfieeImage != value)
                {
                    _selfieeImage = value;
                    OnPropertyChanged(nameof(SelfieeImage));
                }
            }
        }
        private ImageSource _aadharPhotoImage;
        public ImageSource AadharPhotoImage
        {
            get => _aadharPhotoImage;
            set
            {
                if (_aadharPhotoImage != value)
                {
                    _aadharPhotoImage = value;
                    OnPropertyChanged(nameof(AadharPhotoImage));
                }
            }
        }
        private ImageSource _panCardImage;
        public ImageSource PanCardImage
        {
            get => _panCardImage;
            set
            {
                if (_panCardImage != value)
                {
                    _panCardImage = value;
                    OnPropertyChanged(nameof(PanCardImage));
                }
            }
        }
        private ImageSource _passbookAndChequeImage;
        public ImageSource PassbookAndChequeImage
        {
            get => _passbookAndChequeImage;
            set
            {
                if (_passbookAndChequeImage != value)
                {
                    _passbookAndChequeImage = value;
                    OnPropertyChanged(nameof(PassbookAndChequeImage));
                }
            }
        }

        
        private BoDistrict _selectedDistrict;
        public BoDistrict SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                if (_selectedDistrict != value)
                {
                    _selectedDistrict = value;
                    OnPropertyChanged(nameof(SelectedDistrict));

                }
            }
        }

        private string _stateName;
        public string StateName
        {
            get => _stateName;
            set
            {
                if (_stateName != value)
                {
                    _stateName = value;
                    OnPropertyChanged(nameof(StateName));
                }
            }
        }

        public string _districtName;
        public string DistrictName
        {
            get => _districtName;
            set
            {
                if (_districtName != value)
                {
                    _districtName = value;
                    OnPropertyChanged(nameof(DistrictName));
                }
            }
        }
        
        public bool _basicInformationVisible;
        public bool BasicInformationVisible
        {
            get => _basicInformationVisible;
            set
            {
                if (_basicInformationVisible != value)
                {
                    _basicInformationVisible = value;
                    OnPropertyChanged(nameof(BasicInformationVisible));
                }
            }
        }

        public bool _imageSectionsVisible;
        public bool ImageSectionsVisible
        {
            get => _imageSectionsVisible;
            set
            {
                if (_imageSectionsVisible != value)
                {
                    _imageSectionsVisible = value;
                    OnPropertyChanged(nameof(ImageSectionsVisible));
                }
            }
        }

        public ObservableCollection<BoState> GetStateList { get; set; }
        public ObservableCollection<BoDistrict> GetDistrictList { get; set; }

        BaseDataService _baseDataService = new BaseDataService();
        UserRegisterDB _userRegisterDB = new UserRegisterDB();
        UserRegisterModel userRegister = new UserRegisterModel();
        int CreatedUserId = 0;
        // private IBaseDataService _baseDataService { get; set; }
        //public UserRegistrationViewMode(IBaseDataService baseDataService)
        //{
        //    _baseDataService = baseDataService;

        //    GetStateList=new ObservableCollection<BoState>();
        //    GetDistrictList = new ObservableCollection<BoDistrict>();

        //}
        public UserRegistrationViewMode()
        {
            GetStateList=new ObservableCollection<BoState>();
            GetDistrictList = new ObservableCollection<BoDistrict>();
            BasicInformationVisible = true;
            ImageSectionsVisible=false;

            BindStateList();
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedState))
            {
                District(SelectedState.StateCode);
            }
           
        }

        [RelayCommand]
        public async Task SaveSignUp()
        {
            
            if ((string.IsNullOrWhiteSpace(UserName)) || (string.IsNullOrWhiteSpace(EmailID)) || (string.IsNullOrEmpty(AdharNumber)) ||
                 (string.IsNullOrWhiteSpace(PhoneNumber)) || (string.IsNullOrEmpty(AdharNumber)) || (string.IsNullOrEmpty(IFSCNumber)) ||
                 (string.IsNullOrEmpty(NameOfBank)) || (string.IsNullOrEmpty(BankAccountNo)))

            {
                await App.Current.MainPage.DisplayAlert("Enter Data", "Please enter correct values specified Area", "OK");
            }
           // userRegister.userName = UserName;
            if (Regex.Match(UserName, "^[a-zA-Z ]*$").Success)
            {
                userRegister.userName = UserName;
                userRegister.userID = UserName;
            }
            else
            {
                //await App.Current.MainPage.DisplayAlert("", "Enter User Name", "ok");
                // return;
            }

            if (PhoneNumber.Length == 10 )
            {
                userRegister.phoneNo= PhoneNumber;
                userRegister.mobile = PhoneNumber;
                userRegister.password = PhoneNumber;
            }
            else 
            { 
               //  await App.Current.MainPage.DisplayAlert("", "Enter 10 digit Number", "ok");
               // UserName = string.Empty;
               
            }
            if (SelectedState.ToString() != "-1")
            {
                userRegister.State = SelectedState.StateName.ToString();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "select state", "ok");
                return;
            }

            if (SelectedDistrict.DistrictName.ToString() != "-1")
            {
                userRegister.District = SelectedDistrict.DistrictName.ToString();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "select District", "ok");
                return;
            }
            
            if (Regex.Match(EmailID, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                userRegister.email = EmailID; // Assign email if it matches the valid format.
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Enter correct email address", "OK");
                return;
            }

            if (AdharNumber.Length == 12 )
            {
                userRegister.AadhaarNumber= AdharNumber;
            }
            else
            { 
                 await App.Current.MainPage.DisplayAlert("", "Enter correct Aadhar number", "ok");
                return;
            }

            if (Regex.Match(PanCardNo, "^[A-Z]{5}[0-9]{4}[A-Z]{1}").Success)
            {
                userRegister.PanCardNo= PanCardNo;
            }
            else
            { 
                 await App.Current.MainPage.DisplayAlert("", "Enter Pan Card Number", "ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(NameOfBank))
            {
                userRegister.Bank = NameOfBank;
            }
            else
            { 
                 await App.Current.MainPage.DisplayAlert("", "Enter Bank Name", "ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(BankAccountNo))
            {
                userRegister.AccountNo = BankAccountNo;
            }
            else
            { 
                 await App.Current.MainPage.DisplayAlert("", "Enter Bank Account Number", "ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(IFSCNumber))
            {
                userRegister.IFSCCode=IFSCNumber;
            }
            else
            { 
                 await App.Current.MainPage.DisplayAlert("", "Enter Bank IFSCode", "ok");
                return;
            }

            userRegister.imeiNo = "";
           
            userRegister.department = "";

                var result=await _userRegisterDB.AddUserRegisterDetails(userRegister);
                if(result == "Sucessfully Add User")
                {
                    await App.Current.MainPage.DisplayAlert("Successfull!", "User Details Saved!", "ok");
                    BasicInformationVisible = false;
                    ImageSectionsVisible = true;
                }
            
        }

        [RelayCommand]
        public async Task PhotoCapture(object obj)
        {
            var Text = obj.ToString();
            Stream stream1 = null;
            var location = await Geolocation.GetLastKnownLocationAsync();
            try
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    Directory = "ClaimIntimation",
                    SaveToAlbum = true
                });
                stream1 = photo.GetStream();


                if (stream1 != null)
                {
                    using (Stream sourceStream = stream1)
                    {
                        ////var test = Preferences.Get("UserCurrentLocation", "");
                        string latitude = location.Latitude.ToString();
                        string longitude = location.Longitude.ToString();

                        string timestamp = DateTime.Now.ToString();
                        using (var modifiedImageStream = await AddTextToImageAsync(sourceStream, latitude, longitude, timestamp, Text))
                        {
                            DateTime aDate = DateTime.Now;
                            
                            //ImageNameModify
                            var NewImageName = DateTime.Now.ToString("dd:MM:yyy:hh:ms").Replace(":", "") + ".jpg";

                            // Save the modified image to local storage
                            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), NewImageName);
                            await SaveImageAsync(modifiedImageStream, filePath);

                            var displayStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                            if (Text == "Selfiee Photo")
                            {
                                SelfieeImage = ImageSource.FromStream(() => displayStream);
                                userRegister.SelfPhotoPath = filePath;
                            }
                            else if (Text == "Aadhar Photo")
                            {
                                AadharPhotoImage = ImageSource.FromStream(() => displayStream);
                                userRegister.AadharPhotoPath = filePath;

                            }
                            else if (Text == "PanCard Photo")
                            {
                                PanCardImage = ImageSource.FromStream(() => displayStream);
                                userRegister.PanPhotoPath = filePath;
                            }
                            else if (Text == "Passbook or Cheque Photo")
                            {
                                PassbookAndChequeImage = ImageSource.FromStream(() => displayStream);
                                userRegister.PassbookChequePath = filePath;
                            }
                            


                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

        }
        public async Task<Stream> AddTextToImageAsync(Stream imageStream, string latitude, string longitude, string timestamp, string Text)
        {
            // Load the image from the stream
            var bitmap = SKBitmap.Decode(imageStream);

            // Create a new surface
            var surface = SKSurface.Create(new SKImageInfo(bitmap.Width, bitmap.Height));

            // Get the canvas
            var canvas = surface.Canvas;

            // Draw the original image
            canvas.DrawBitmap(bitmap, 0, 0);

            // Define the text to add
            var texts = new[]
            {
                $"Latitude: {latitude}",
                $"Longitude: {longitude}",
                $"Timestamp: {timestamp}",
                $"Caption: {Text}"
            };

            // Calculate dynamic text size based on image dimensions
            float textSize = bitmap.Height / 40f; // Adjust this factor as needed

            // Define the font and text properties
            var paint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Yellow,
                TextSize = textSize,
                Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
                TextAlign = SKTextAlign.Left,
                StrokeCap = SKStrokeCap.Square,
            };

            var x = bitmap.Width / 25; // 10 pixels from the left edge
            var y = bitmap.Height - (int)(textSize * 2.5); // Adjust based on text size

            // Draw each line of text
            foreach (var text in texts)
            {
                canvas.DrawText(text, x, y, paint);
                y -= (int)(textSize * 1.5); // Move down to the next line
            }

            // Save the modified image to a stream
            var modifiedImage = surface.Snapshot();
            var tempFilePath = System.IO.Path.GetTempFileName();
            using (var modifiedImageStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                await modifiedImage.Encode(SKEncodedImageFormat.Jpeg, 70).AsStream().CopyToAsync(modifiedImageStream);
            }

            // Return a new FileStream that is readable
            return new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
        }

        public async Task SaveImageAsync(Stream imageStream, string filePath)
        {
            var directory = System.IO.Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }
        }

        [RelayCommand]
        public async Task ImageUploadToServer(object obj)
        {
            var paramiter = obj as UserRegisterModel;

            if (userRegister != null)
            {
                //UserRegisterModel user = new UserRegisterModel();

                //user.userName = paramiter.userName;
                //user.phoneNo = paramiter.phoneNo;
                //user.State = paramiter.State;
                //user.District = paramiter.District;
                //user.email = paramiter.email;
                //user.AadhaarNumber = paramiter.AadhaarNumber;
                //user.PanCardNo = paramiter.PanCardNo;
                //user.Bank = paramiter.Bank;
                //user.AccountNo = paramiter.AccountNo;
                //user.IFSCCode = paramiter.IFSCCode;

                if (!string.IsNullOrWhiteSpace(userRegister.SelfPhotoPath))
                {
                   // userRegister.SelfPhotoPath = userRegister.SelfPhotoPath;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert!", "Click Self Photo ", "ok");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(userRegister.AadharPhotoPath))
                {
                    //userRegister.AadharPhotoPath = paramiter.AadharPhotoPath;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert!", "Click Aadhar Photo ", "ok");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(userRegister.PanPhotoPath))
                {
                   // userRegister.PanPhotoPath = paramiter.PanPhotoPath;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert!", "Click PanCard Photo ", "ok");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(userRegister.PassbookChequePath))
                {
                   // userRegister.PassbookChequePath = paramiter.PassbookChequePath;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert!", "Click Passbook & Checkbook Photo ", "ok");
                    return;
                }

                try
                {

                    var client = new HttpClient();
                    client.BaseAddress = new Uri(GlobalVariables.appUrl);
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(userRegister);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("/api/UserManager/Register", content);

                    // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                    var result = await response.Content.ReadAsStringAsync();
                    CreatedUserId = int.Parse(result);
                    //var retrunvalue = userDB.AddUser(users);

                    if (int.Parse(result) > 0)
                    {
                        HttpClient client1 = new HttpClient();

                        if (!string.IsNullOrWhiteSpace(userRegister.SelfPhotoPath))
                        {

                            //FilePath = users.SelfPhotoPath;

                            var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                            try
                            {
                                //read file into upfilebytes array
                                var fileBytes = File.ReadAllBytes(userRegister.SelfPhotoPath);

                                var name = Path.GetFileName(userRegister.SelfPhotoPath);
                                var newFileName = "UserProfile_" + CreatedUserId + "_" + name;
                                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId

                                ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                                MultipartFormDataContent content1 = new MultipartFormDataContent();
                                content1.Add(byteContent, "File", newFileName);

                                //upload MultipartFormDataContent content async and store response in response var
                                var response1 = await client.PostAsync(url, content1);

                                //read response result as a string async into json var
                                var responsestr = response1.Content.ReadAsStringAsync().Result;

                                //await DisplayAlert("Self photo", "Image Uploaded", "ok");

                            }
                            catch (Exception ex)
                            {

                                return;
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(userRegister.AadharPhotoPath))
                        {

                            //FilePath = users.AadharPhotoPath;
                            var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                            try
                            {
                                //read file into upfilebytes array
                                var fileBytes = File.ReadAllBytes(userRegister.AadharPhotoPath);

                                var name = Path.GetFileName(userRegister.AadharPhotoPath);
                                var newFileName = "UserProfile_" + CreatedUserId + "_" + name;
                                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                                //HttpClient client = new HttpClient();
                                //MultipartFormDataContent content = new MultipartFormDataContent();
                                ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                                MultipartFormDataContent content1 = new MultipartFormDataContent();
                                content1.Add(byteContent, "File", newFileName);


                                //upload MultipartFormDataContent content async and store response in response var
                                var response1 = await client.PostAsync(url, content1);

                                //read response result as a string async into json var
                                var responsestr = response1.Content.ReadAsStringAsync().Result;

                                //await DisplayAlert("Aadhar Photo", "Image Uploaded", "ok");

                            }
                            catch (Exception ex)
                            {

                                await App.Current.MainPage.DisplayAlert("Aadhar Photo", ex.Message, "ok");
                                return;
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(userRegister.PanPhotoPath))
                        {

                            var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                            try
                            {
                                //read file into upfilebytes array
                                var fileBytes = File.ReadAllBytes(userRegister.PanPhotoPath);

                                var name = Path.GetFileName(userRegister.PanPhotoPath);
                                var newFileName = "UserProfile_" + CreatedUserId + "_" + name;
                                ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                                MultipartFormDataContent content1 = new MultipartFormDataContent();
                                content1.Add(byteContent, "File", newFileName);

                                var response1 = await client.PostAsync(url, content1);

                                var responsestr = response1.Content.ReadAsStringAsync().Result;

                                //await DisplayAlert("Pan Card", "Image Uploaded", "ok");

                            }
                            catch (Exception ex)
                            {
                                await App.Current.MainPage.DisplayAlert("Pan Card", ex.Message, "ok");
                                return;
                            }

                        }
                        if (!string.IsNullOrWhiteSpace(userRegister.PassbookChequePath))
                        {

                            //FilePath = users.PassbookChequePath;
                            var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                            try
                            {
                                //read file into upfilebytes array
                                var fileBytes = File.ReadAllBytes(userRegister.PassbookChequePath);

                                var name = Path.GetFileName(userRegister.PassbookChequePath);
                                var newFileName = "UserProfile_" + CreatedUserId + "_" + name;
                                //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                                //HttpClient client = new HttpClient();
                                //MultipartFormDataContent content = new MultipartFormDataContent();
                                ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                                MultipartFormDataContent content1 = new MultipartFormDataContent();
                                content1.Add(byteContent, "File", newFileName);


                                //upload MultipartFormDataContent content async and store response in response var
                                var response1 = await client.PostAsync(url, content1);

                                //read response result as a string async into json var
                                var responsestr = response1.Content.ReadAsStringAsync().Result;

                                //await DisplayAlert("Passbook", "Image Uploaded", "ok");

                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString();
                            }

                        }

                        await App.Current.MainPage.DisplayAlert("Success!", "Record has been saved!" , "ok");


                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return;
                }
            }
           


        }


        [RelayCommand]
        public async Task ExecuteBack()
        {
            try
            {
                Application.Current.MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private async Task BindStateList()
        {
            try
            {
                GetStateList.Clear();

                var stateitem = await _baseDataService.GetState();
                if (stateitem != null)
                {
                    foreach (var item in stateitem)
                    {
                        BoState boState = new BoState()
                        {
                            StateCode = item.StateCode,
                            StateName = item.StateName.Trim() + " (" + item.StateCode.Trim() + ")"
                        };
                        GetStateList.Add(boState);
                    }
                    // ProposalBasicDetails.SelectedState = Distric();
                }



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("State List", ex.Message.ToString(), "OK");
            }

        }

        public async Task District(string StateCode)
        {
            GetDistrictList.Clear();
            if (StateCode != "")
            {
                try
                {

                    var data = await _baseDataService.GetDistric(StateCode);

                    foreach (var tag in data)
                    {
                        BoDistrict boDistrict = new BoDistrict()
                        {
                            DistrictId = tag.DistrictId,
                            DistrictName = tag.DistrictName,
                        };
                        GetDistrictList.Add(boDistrict);
                    }

                }
                catch (Exception ex)
                {

                }
            }


        }

    }
}
