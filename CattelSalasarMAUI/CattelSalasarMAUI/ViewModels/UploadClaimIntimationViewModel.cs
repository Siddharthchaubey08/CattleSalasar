using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.SQLiteDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class UploadClaimIntimationViewModel : BaseViewModel, IQueryAttributable
    {
        #region PropertyResigion

        private string _leadNumber;
        public string LeadNumber
        {
            get => _leadNumber;
            set
            {
                if (_leadNumber != value)
                {
                    _leadNumber = value;
                    OnPropertyChanged(nameof(LeadNumber));
                }
            }
        }

        private string _typeOfAnimal;
        public string TypeOfAnimal
        {
            get => _typeOfAnimal;
            set
            {
                if (_typeOfAnimal != value)
                {
                    _typeOfAnimal = value;
                    OnPropertyChanged(nameof(TypeOfAnimal));
                }
            }
        }
        private string _tagNumber;
        public string TagNumber
        {
            get => _tagNumber;
            set
            {
                if (_tagNumber != value)
                {
                    _tagNumber = value;
                    OnPropertyChanged(nameof(TagNumber));
                }
            }
        }

        private string _claimProposalId;
        public string ClaimProposalId
        {
            get => _claimProposalId;
            set
            {
                if (_claimProposalId != value)
                {
                    _claimProposalId = value;
                    OnPropertyChanged(nameof(ClaimProposalId));
                }
            }
        }
        private bool _ClaimCardPageLoader;
        public bool ClaimCardPageLoader
        {
            get => _ClaimCardPageLoader;
            set
            {
                if (_ClaimCardPageLoader != value)
                {
                    _ClaimCardPageLoader = value;
                    OnPropertyChanged(nameof(ClaimCardPageLoader));
                }
            }
        }

        private ClaimAnimalImageCardClass _uploadClaimAnimalImageCard;
        public ClaimAnimalImageCardClass UploadClaimAnimalImageCard
        {
            get => _uploadClaimAnimalImageCard;
            set
            {
                if (_uploadClaimAnimalImageCard != value)
                {
                    _uploadClaimAnimalImageCard = value;
                    OnPropertyChanged(nameof(UploadClaimAnimalImageCard));
                }
            }
        }
    #endregion

        [ObservableProperty]
        ObservableCollection<UploadClaimCardModel> _getClaimIntimationCardList;
        ClaimImageDB _claimImageDB = new ClaimImageDB();
        public IClaimIntimationService _claimIntimationService { get; set; }
        public UploadClaimIntimationViewModel(IClaimIntimationService claimIntimationService)
        {
            _claimIntimationService=claimIntimationService;
            GetClaimIntimationCardList = new ObservableCollection<UploadClaimCardModel>();
            UploadClaimAnimalImageCard = new ClaimAnimalImageCardClass();
            UploadClaimAnimalImageCard.LocalSaveButtomVisible = false;
            ClaimCardPageLoader = true;

            UploadClaimAnimalImageCard.ClaimDetailsPageVisible = true;
            UploadClaimAnimalImageCard.ClaimCardPageLoader = false;
           
            OnAppringMethod();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("UploadClaimDataKeys"))
            {
                var serializedModel = query["UploadClaimDataKeys"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    serializedModel = Uri.UnescapeDataString(serializedModel);
                    var data = JsonConvert.DeserializeObject<UploadClaimCardModel>(serializedModel);
                    // Your existing logic
                    var LeadNumber = data.LeadNumber;
                    var ClaimProposalId = data.ClaimProposalId;
                    var TagNumber = data.TagNumber;
                    var TypeOfAnimal = data.TypeOfAnimal;

                    await GetUploadClaimIntimation(LeadNumber, ClaimProposalId, TagNumber, TypeOfAnimal);

                }
            }
        }

        public async Task OnAppringMethod()
        {
            ClaimCardPageLoader = true;
            GetClaimIntimationCardList.Clear();
            var data = await _claimIntimationService.GetUpdateClaimIntimationCardList();
            if(data !=null)
            {
               
                foreach (var item in data)
                {
                    UploadClaimCardModel claimCardModel = new UploadClaimCardModel()
                    {
                        LeadNumber = item.LeadNumber,
                        ClaimProposalId = item.ClaimProposalId,
                        TagNumber = item.TagNumber,
                        TypeOfAnimal = item.TypeOfAnimal,
                        

                    };
                    GetClaimIntimationCardList.Add(claimCardModel);
                }
                ClaimCardPageLoader = false;
            }
            else
            {
                ClaimCardPageLoader = false;
                await App.Current.MainPage.DisplayAlert("Alert!", "Claim data not available!", "OK");

            }
           
        }

        [RelayCommand]
        public async Task TappedClaimDataList(object obj)
        {
            try
            {
                var paramiter = obj as UploadClaimCardModel;
                UploadClaimCardModel dataModel = new UploadClaimCardModel()
                {
                    LeadNumber = paramiter.LeadNumber,
                    ClaimProposalId = paramiter.ClaimProposalId,
                    TagNumber = paramiter.TagNumber,
                    TypeOfAnimal = paramiter.TypeOfAnimal
                };

                if (dataModel != null)
                {
                    var serializedModel = JsonConvert.SerializeObject(dataModel); // Serialize the model
                    var encodedModel = Uri.EscapeDataString(serializedModel);     // Encode the serialized model
                    await Shell.Current.GoToAsync($"uploadClaimIntimationCard/uploadClaimIntimation?UploadClaimDataKeys={encodedModel}");

                    //await Shell.Current.GoToAsync($"//uploadClaimIntimationCard/uploadClaimIntimation?UploadClaimDataKeys={encodedModel}");

                }

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

        }

       
        public async Task GetUploadClaimIntimation(string LeadNumber, string ClaimProposalId, string TagNumber, string TypeOfAnimal)
        {
            try
            {
                var ImageList1 =await _claimImageDB.GetClaimIntimationImage();
               // var ImageList1 =await _claimImageDB.DeleteImageDetailsByAnimalId();
                var ImageList = ImageList1.Where(x => x.LeadNumber == LeadNumber && x.TagNumber == TagNumber).ToList();

                //var ImageList =await _claimImageDB.GetUploadImageDetails(LeadNumber, ClaimProposalId, TagNumber);
                foreach (var item in ImageList)
                {
                    UploadClaimAnimalImageCard.LeadNumber = LeadNumber;
                    UploadClaimAnimalImageCard.ClaimProposalId = ClaimProposalId;
                    UploadClaimAnimalImageCard.TagNumber = TagNumber;
                    UploadClaimAnimalImageCard.TypeOfAnimal = item.TypeOfAnimal;

                    var imageBytes = await File.ReadAllBytesAsync(item.ImgesPath);
                    var imageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));

                    if (item.ImageCapson == "Clear Tag Number Visible Photo")
                    {
                        UploadClaimAnimalImageCard.FirstImage = imageSource;
                    }
                    else if (item.ImageCapson == "Right Side (Right side of the Animal)")
                    {
                        UploadClaimAnimalImageCard.SecondImage = imageSource;

                    }
                    else if (item.ImageCapson == "Left Side (Right side of the Animal)")
                    {
                        UploadClaimAnimalImageCard.ThirdImage = imageSource;

                    }
                    else if (item.ImageCapson == "Front of Animal")
                    {
                        UploadClaimAnimalImageCard.FourthImage = imageSource;

                    }
                    else if (item.ImageCapson == "Back side of Animal")
                    {
                        UploadClaimAnimalImageCard.FivethImage = imageSource;

                    }
                    else if (item.ImageCapson == "Along with the Owner/ Proposer")
                    {
                        UploadClaimAnimalImageCard.SixthImage = imageSource;

                    }
                    else if (item.ImageCapson == "Veterinary Health Certificate")
                    {
                        UploadClaimAnimalImageCard.SeventhImage = imageSource;

                    }
                    else if (item.ImageCapson == "Proposal Form")
                    {
                        UploadClaimAnimalImageCard.EighthImage = imageSource;

                    }
                    else if (item.ImageCapson == "Aadhar card")
                    {
                        UploadClaimAnimalImageCard.NinthImage = imageSource;
                        
                    }
                    else if (item.ImageCapson == "UTR Image")
                    {
                        UploadClaimAnimalImageCard.TenthImage = imageSource;
                    }
                    else if (item.ImageCapson == "Post-Mortem Report")
                    {

                        UploadClaimAnimalImageCard.EleventhImage = imageSource;

                    }
                    else if (item.ImageCapson == "Beneficiary Bank Passbook")
                    {

                        UploadClaimAnimalImageCard.TwelthImage = imageSource;

                    }
                    else if (item.ImageCapson == "Upload File")
                    {
                        //UploadClaimAnimalImageCard.TwelthImage = imageSource;

                    }
                }
                

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

            

        }

        //Server Upload
        [RelayCommand]
        public async Task ImageUploadToServer(object obj)
        {
            UploadClaimAnimalImageCard.ClaimCardPageLoader = true;
            UploadClaimAnimalImageCard.ClaimDetailsPageVisible = false; //Page UI visible False
            var paramiter = obj as ClaimAnimalImageCardClass;
            try
            {

                ObservableCollection<ClaimIntimationImageModel> ClaimImageDetails = new ObservableCollection<ClaimIntimationImageModel>();
                ClaimIntimationImageModel images1 = new ClaimIntimationImageModel();
                ClaimImageDetails.Clear();

                var LeadNo = paramiter.LeadNumber;
                var ClaimProposalId = paramiter.ClaimProposalId;
                var TagNumber = paramiter.TagNumber;

                var ImageList = await _claimImageDB.GetUploadImageDetails(LeadNo, ClaimProposalId, TagNumber);

               
                for (int i = 0; i < ImageList.Count(); i++)
                {
                    images1.ImgesPath = ImageList.ToArray()[i].ImgesPath;
                    images1.ImageName = ImageList.ToArray()[i].ImageName;
                    images1.ClaimIntimationId = ImageList.ToArray()[i].ClaimIntimationId;
                    images1.CompassDegrees = ImageList.ToArray()[i].CompassDegrees;
                    images1.CreatedDate = ImageList.ToArray()[i].CreatedDate; //tdata.ToString();
                    images1.Latitude = ImageList.ToArray()[i].Latitude;
                    images1.Longitude = ImageList.ToArray()[i].Longitude;
                    images1.LeadNumber = ImageList.ToArray()[i].LeadNumber;
                    images1.ClaimProposalId = ImageList.ToArray()[i].ClaimProposalId;
                    images1.TagNumber = ImageList.ToArray()[i].TagNumber;
                    images1.TypeOfAnimal = ImageList.ToArray()[i].TypeOfAnimal;
                    images1.ImageCapson = ImageList.ToArray()[i].ImageCapson;
                    images1.TimeStamp = ImageList.ToArray()[i].TimeStamp;
                }

                ClaimImageDetails.Add(images1);
               
                var retrunvalue = await _claimIntimationService.SaveClaimIntimationImagesOnServer(ClaimImageDetails);
               
                if (retrunvalue == true)
                {

                    await App.Current.MainPage.DisplayAlert("Message", "Image Saved Sucessfuly", "OK");
                    
                    //Clearn Image Source
                    ClearMethod();

                }
                if (ImageList != null)
                {
                    //await DisplayAlert("", "Proposal Details Uploaded", "ok");

                    int uploadimagescount = 0;
                    for (int i = 0; i < ImageList.Count(); i++)
                    {
                        var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                        uploadimagescount++;

                        try
                        {
                            //read file into upfilebytes array
                            var fileBytes = File.ReadAllBytes(ImageList.ToArray()[i].ImgesPath);
                            var ProposalId = ImageList.ToArray()[i].ClaimProposalId;
                            var name = Path.GetFileName(ImageList.ToArray()[i].ImgesPath);
                            var newFileName = "Claim_" + ProposalId + "_" + name;
                            //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                            HttpClient client = new HttpClient();
                            MultipartFormDataContent content = new MultipartFormDataContent();
                            ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                            content.Add(byteContent, "File", newFileName);


                            //upload MultipartFormDataContent content async and store response in response var
                            var response =
                                await client.PostAsync(url, content);

                            //read response result as a string async into json var
                            var responsestr = response.Content.ReadAsStringAsync().Result;

                            //debug
                            //Debug.WriteLine(responsestr);
                            if (ImageList.Count() == uploadimagescount)
                            {
                                await App.Current.MainPage.DisplayAlert("ClaimIntimation: " + ProposalId, "Claim Intimation Images Uploaded", "ok");
                               
                                GlobalVariables.checkReturnSamepage = 1;
                                //await Navigation.PushAsync(new ProposalUpload());
                               // GetUploadList();
                            }

                        }
                        catch (Exception ex)
                        {
                            //debug
                            //Debug.WriteLine("Exception Caught: " + e.ToString());

                            return;
                        }
                        //Images.Add(new ProposalImages { ProposalImagesId = imageList.ToArray()[0].ProposalImagesId, ProposalId = imageList.ToArray()[0].ProposalId, ImgesPath = imageList.ToArray()[0].ImgesPath, Name = imageList.ToArray()[0].Name });
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Claim Intimation Details Not Uploaded", "ok");
                   
                }
                var ImageLists = await _claimImageDB.DeleteImageDetailsByAnimalId(LeadNo, ClaimProposalId, TagNumber);
                UploadClaimAnimalImageCard.ClaimCardPageLoader = false;
                UploadClaimAnimalImageCard.ClaimDetailsPageVisible = true;

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

        }
        public void ClearMethod()
        {
            UploadClaimAnimalImageCard.FirstImage = " ";
            UploadClaimAnimalImageCard.SecondImage =" ";
            UploadClaimAnimalImageCard.ThirdImage =" ";
            UploadClaimAnimalImageCard.FourthImage =" ";
            UploadClaimAnimalImageCard.FivethImage =" ";
            UploadClaimAnimalImageCard.SixthImage =" ";
            UploadClaimAnimalImageCard.SeventhImage =" ";
            UploadClaimAnimalImageCard.EighthImage =" ";
            UploadClaimAnimalImageCard.NinthImage =" ";
            UploadClaimAnimalImageCard.TenthImage =" ";
        }

    }

    public class UploadClaimCardModel()
    {
        public int Id { get; set; }
        public string LeadNumber { get; set; }
        public string TypeOfAnimal { get; set; }
        public string TagNumber { get; set; }
        public string ClaimProposalId { get; set; }
    }
}
