using CattelSalasarMAUI.Helper;
using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.SQLiteDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static SQLite.SQLite3;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class ProposalAnimalDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        ProposalAnimalDetailClass _animalDetails;
       
        [ObservableProperty]
        private ProposalAnimalDetailClass _uploadProposalDetailsView;
        
        [ObservableProperty]
        private ProposalAnimalDetailClass _editProposalAnimalDetails;
        
        [ObservableProperty]
        AnimalDataCardClass _proposalAnimalCardView;
         
        [ObservableProperty]
        ObservableCollection<ProposalImage> _editTappedImagelst;

        private string _toggelButtonValue;
        public string ToggelButtonValue
        {
            get => _toggelButtonValue;
            set
            {
                if (_toggelButtonValue != value)
                {
                    _toggelButtonValue = value;
                    OnPropertyChanged(nameof(ToggelButtonValue));
                }
            }
        }
        
        private AnimalDataCardClass _editAnimalCardView;
        public AnimalDataCardClass EditAnimalCardView
        {
            get => _editAnimalCardView;
            set
            {
                if (_editAnimalCardView != value)
                {
                    _editAnimalCardView = value;
                    OnPropertyChanged(nameof(EditAnimalCardView));
                }
            }
        }

        private bool _editCardPageLoaderEnable;
        public bool EditCardPageLoaderEnable
        {
            get => _editCardPageLoaderEnable;
            set
            {
                if (_editCardPageLoaderEnable != value)
                {
                    _editCardPageLoaderEnable = value;
                    OnPropertyChanged(nameof(EditCardPageLoaderEnable));
                }
            }
        }
        private bool _uploadAnimalCardLoaderEnable;
        public bool UploadAnimalCardLoaderEnable
        {
            get => _uploadAnimalCardLoaderEnable;
            set
            {
                if (_uploadAnimalCardLoaderEnable != value)
                {
                    _uploadAnimalCardLoaderEnable = value;
                    OnPropertyChanged(nameof(UploadAnimalCardLoaderEnable));
                }
            }
        }


         private bool _editAnimalPageLoaderEnable;
        public bool EditAnimalPageLoaderEnable
        {
            get => _editAnimalPageLoaderEnable;
            set
            {
                if (_editAnimalPageLoaderEnable != value)
                {
                    _editAnimalPageLoaderEnable = value;
                    OnPropertyChanged(nameof(EditAnimalPageLoaderEnable));
                }
            }
        }


        private AnimalDataModel _editAnimalSingeTapped;
        public AnimalDataModel EditAnimalSingeTapped
        {
            get => _editAnimalSingeTapped;
            set
            {
                if (_editAnimalSingeTapped != value)
                {
                    _editAnimalSingeTapped = value;
                    OnPropertyChanged(nameof(EditAnimalSingeTapped));
                }
            }
        }

        [ObservableProperty]
        private string _leadNo;
        [ObservableProperty]
        private int _proposelId;
        [ObservableProperty]
        private string _propIds;
        [ObservableProperty]
        private string _editSpecise;
        [ObservableProperty]
        private string _noOfCattleToBeInsured;

        int Cattelflag = 1;
        AnimalDetailDB _animalDetailsDB=new AnimalDetailDB();
        ProposalImageDB _proposalImageDB = new ProposalImageDB();
        public string ParamiterData { get; set; }

        AnimalDetailDB _animalDetailDB = new AnimalDetailDB();

        private IUploadProposalService _uploadAnimalDetailToServer;
        private IEditProposalService _editProposalService { get; set; }
        public ProposalAnimalDetailsViewModel(IUploadProposalService uploadProposalService, IEditProposalService editProposalService)
        {
            _uploadAnimalDetailToServer = uploadProposalService;
            _editProposalService = editProposalService;
            EditAnimalCardView = new AnimalDataCardClass();
            EditProposalAnimalDetails = new ProposalAnimalDetailClass();
            EditTappedImagelst = new ObservableCollection<ProposalImage>();

            AnimalDetails = new ProposalAnimalDetailClass();
            UploadProposalDetailsView = new ProposalAnimalDetailClass();
            //AnimalDetails.ImageSection = true;
            AnimalDetails.AnimalDetailsSection = true;
            AnimalDetails.ImageSection = false;
            UploadProposalDetailsView.AnimalDetailsSection = true;
            UploadProposalDetailsView.ImageSection = false;
            UploadProposalDetailsView.ImageSaveToServerVisible = true;
            UploadProposalDetailsView.ImageSaveToDbVisible = false;
            UploadAnimalCardLoaderEnable = false;

            EditProposalAnimalDetails.AnimalDetailsSection = true;
            EditProposalAnimalDetails.ImageSection = false;
            EditProposalAnimalDetails.ImageSaveToServerVisible = true;
            EditProposalAnimalDetails.ImageSaveToDbVisible = false;
            
            AnimalDetails.TestComman = "CreateAnimalDetails";
            UploadProposalDetailsView.TestComman = "UpdateAnimalDetails";
            EditProposalAnimalDetails.TestComman = "EditAnimalDetails";
            EditAnimalPageLoaderEnable = false;
            ProposalAnimalCardView = new AnimalDataCardClass();
            UploadBasicDetailsMethod(LeadNo);
            EditAnimalCardList();

            //Image Toggle
            CameraToggelClicked("CameraSelect");
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("basicdetailsKey"))
            {
                var serializedModel = query["basicdetailsKey"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {

                    serializedModel = Uri.UnescapeDataString(serializedModel);
                    var data = JsonConvert.DeserializeObject<CreateProposalResult>(serializedModel);
                    // Your existing logic
                    LeadNo = data.LeadNumber;
                    PropIds = data.PropId;
                    NoOfCattleToBeInsured = data.SumInsured;
                    AnimalDetails.LeadNumber = LeadNo;
                    AnimalDetails.CurrentAnimalNumber = Cattelflag.ToString();
                }
            }
            if (query.ContainsKey("uploadbasicdetailsKeys"))
            {
                var serializedModel = query["uploadbasicdetailsKeys"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    var LeadNo = serializedModel;
                    Preferences.Set("TempLeadNo", LeadNo);
                    UploadBasicDetailsMethod(LeadNo);
                }
            }
            // For upload details page
            if (query.ContainsKey("basicAnimalDataKeys"))
            {
                var serializedModel = query["basicAnimalDataKeys"] as string;

                if (!string.IsNullOrEmpty(serializedModel))
                {
                    try
                    {
                        // Decode and deserialize the JSON string
                        var decodedModel = Uri.UnescapeDataString(serializedModel);
                        var dataModel = JsonConvert.DeserializeObject<DataModelPareamiter>(decodedModel);

                        if (dataModel != null)
                        {
                            // Assign values from the deserialized object
                            LeadNo = dataModel.LeadNumber;
                            var SpeciesNo = dataModel.Species;
                            PropIds = SpeciesNo;
                            ProposelId = dataModel.PropId;

                            // Make image save button visible
                            UploadProposalDetailsView.ImageSaveToServerVisible = true;
                            UploadProposalDetailsView.ImageSaveToDbVisible = false;

                            // Call TestMethod with the deserialized values
                            TestMethod(ProposelId, LeadNo, SpeciesNo);
                        }
                    }
                    catch(Exception ex)
                    {
                        ex.Message.ToString();
                    }
                 }
            }

            //Edit CardView
            if (query.ContainsKey("EditAnimaldetailsKeys"))
            {
                var serializedModel = query["EditAnimaldetailsKeys"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    var decodedModel = Uri.UnescapeDataString(serializedModel);
                    var dataModel = JsonConvert.DeserializeObject<CreateProposalResult>(decodedModel);
                    if(dataModel !=null)
                    {
                        var ProposalId = dataModel.PropId;
                        var LeadNumber = dataModel.LeadNumber;
                        var CattleTobeInsured = dataModel.SumInsured;
                        Preferences.Set("EditAnimalCardProposalId", ProposalId);
                        await EditAnimalCardList();
                        
                    }
                }
            }
            if (query.ContainsKey("AnimalPageDataKeys"))
            {
                var serializedModel = query["AnimalPageDataKeys"] as string;

                if (!string.IsNullOrEmpty(serializedModel))
                {
                    try
                    {
                        // Decode and deserialize the JSON string
                        var decodedModel = Uri.UnescapeDataString(serializedModel);
                        var dataModel = JsonConvert.DeserializeObject<DataModelPareamiter>(decodedModel);

                        if (dataModel != null)
                        {
                            LeadNo = dataModel.LeadNumber;
                            var SpeciesNo = dataModel.Species;
                            EditSpecise = SpeciesNo;
                            ProposelId = dataModel.PropId;

                            // Call TestMethod with the deserialized values
                            await EditDataVisiblePage(ProposelId, LeadNo, EditSpecise);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }
            }

        }

        //EditAnimalCard
        private async Task EditAnimalCardList()
        {
            var ProposalId = Preferences.Get("EditAnimalCardProposalId", "");
            EditCardPageLoaderEnable = true;
            var data = await _editProposalService.GetEditProposalList(ProposalId);
            EditAnimalCardView.CardAnimalDetailsList.Clear();
            foreach (var item in data.animalData)
            {
                AnimalDataModel animal = new AnimalDataModel()
                {
                    AnimalDataID = item.AnimalDataID,
                    Propid = item.Propid,
                   // LeadNumber = item.LeadNumber,
                    BreedName = item.BreedName,
                    TypeofAnimal = item.TypeofAnimal,
                    Species = item.AnimalDataID.ToString(),
                    Category = item.Category,
                    Sex = item.Sex,
                    BodyColour = item.BodyColour,
                    SwitchOfTail = item.SwitchOfTail,
                    MilkYield = item.MilkYield,
                    MilkingStatus = item.MilkingStatus,
                    PregnancyStatus = item.PregnancyStatus,
                    ApproxAgeOfAnimal = item.ApproxAgeOfAnimal,
                    OtherIdentificationMark = item.OtherIdentificationMark,
                    Vaccination = item.Vaccination,
                    RegistrationNo = item.RegistrationNo,
                    DrContactName = item.DrContactName,
                    DrContactNo = item.DrContactNo,
                    HealthCertificateIssueDate = item.HealthCertificateIssueDate,
                    TagNo = item.TagNo,
                    TypeTagging = item.TypeTagging,
                    Ownership = item.Ownership,
                    PaymentRefNo = item.PaymentRefNo,
                    PremiumRate = item.PremiumRate,
                    SumInsured = item.SumInsured,
                    PremiumAmount = item.PremiumAmount
                };
                EditAnimalCardView.CardAnimalDetailsList.Add(animal);
            }
            EditCardPageLoaderEnable = false;

        }
        //EditAnimalPageOpen
        [RelayCommand]
        private async Task EditAnimalDataViewTapped(object obj)
        {
           // AnimalDataModel SelectedAnimalDataItems
            try
            {
                EditAnimalSingeTapped = obj as AnimalDataModel;
                DataModelPareamiter dataModel = new DataModelPareamiter()
                {
                    LeadNumber = EditAnimalSingeTapped.LeadNumber,
                    Species = EditAnimalSingeTapped.Species,
                    PropId = EditAnimalSingeTapped.Propid
                };

                if (dataModel != null)
                {
                    var serializedModel = JsonConvert.SerializeObject(dataModel);
                    var encodedModel = Uri.EscapeDataString(serializedModel);

                    // Pass the serialized and encoded data as a query parameter
                    await Shell.Current.GoToAsync($"//editPreviewPage/editAnimalCardPage/editAnimalDetailsPage?AnimalPageDataKeys={encodedModel}");

                }

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        public async Task EditDataVisiblePage(int ProposelId,string LeadNo, string EditSpecise)
        {
            var PropId = Convert.ToString(ProposelId);
            var data = await _editProposalService.GetEditProposalList(PropId);
            //var item = data.animalData.Where(x => x.Propid == ProposelId && x.Species == EditSpecise).FirstOrDefault();
            var filteredData = data.animalData.Where(x => x.Propid == ProposelId);
            var item = filteredData.FirstOrDefault(x => x.AnimalDataID == int.Parse(EditSpecise));
            
            if (item !=null)
            {
                EditProposalAnimalDetails.AnimalDataID = item.AnimalDataID.ToString();
                EditProposalAnimalDetails.PropId = item.Propid.ToString();
                EditProposalAnimalDetails.LeadNumber = Preferences.Get("EditProposalSaveLeadId", ""); //Copy throw BasicDetails Page
                EditProposalAnimalDetails.BreedName = item.BreedName;
                EditProposalAnimalDetails.CurrentAnimalNumber = EditSpecise; //opsanal
                EditProposalAnimalDetails.SelectedAnimalType = item.TypeofAnimal;
                EditProposalAnimalDetails.FirstImage = item.Species;
                EditProposalAnimalDetails.SelectedCategoryType = item.Category;
                EditProposalAnimalDetails.SelectedSex = item.Sex;
                EditProposalAnimalDetails.BodyColour = item.BodyColour;
                EditProposalAnimalDetails.SelectedSwitchOfTail = item.SwitchOfTail;
                EditProposalAnimalDetails.MilkYield = item.MilkYield;
                EditProposalAnimalDetails.MilkingStatus = item.MilkingStatus;
                EditProposalAnimalDetails.SelectedPregnancyStatus = item.PregnancyStatus;
                EditProposalAnimalDetails.ApproxAgeOfAnimal = item.ApproxAgeOfAnimal;
                EditProposalAnimalDetails.OtherIdentificationMark = item.OtherIdentificationMark;
                EditProposalAnimalDetails.SelectedVaccination = item.Vaccination;
                EditProposalAnimalDetails.RegistrationNumber = item.RegistrationNo;
                EditProposalAnimalDetails.DrContactName = item.DrContactName;
                EditProposalAnimalDetails.DrContactNo = item.DrContactNo;
                EditProposalAnimalDetails.HealthCertificateIssueDate = item.HealthCertificateIssueDate;
                EditProposalAnimalDetails.TagNumber = item.TagNo;
                EditProposalAnimalDetails.SelectedTypeOfTagging = item.TypeTagging;
                EditProposalAnimalDetails.SelectedOwnershipDetails = item.Ownership;
                EditProposalAnimalDetails.PaymentUTRNumber = item.PaymentRefNo;
                EditProposalAnimalDetails.PremiumRate = item.PremiumRate;
                EditProposalAnimalDetails.SumInsured = item.SumInsured;
                EditProposalAnimalDetails.PremiumAmount = item.PremiumAmount;

            }

            
            // var data = await _editProposalService.GetEditProposalList(PropId);
            //Image Details
            foreach (var item1 in data.proposalImages)
            {
                ProposalImage proposalImage = new ProposalImage()
                {
                    ImageId = item1.ImageId,
                    PropId = item1.PropId,
                    ImageName = item1.ImageName,
                    Latitude = item1.Latitude,
                    Longitude = item1.Longitude,
                    TimeStamp = item1.TimeStamp,
                    CompassDegrees = item1.CompassDegrees,
                    ImageCapson = item1.ImageCapson
                };
                EditTappedImagelst.Add(proposalImage);
            }

            var imageName = EditTappedImagelst.Where(x => x.CompassDegrees == EditSpecise);
            foreach (var name in imageName)
            {
               await FetchImage(name.ImageName, name.ImageCapson);

            }
           
        }

        private async Task FetchImage(string imageName, string imageCapson)
        {
            try
            {
                // Define the base URL
                //var urlbase = GlobalVariables.appUrl+ $"api/FileManager/api/image/{imageName}";
                // var imageUrl = $"http://1.22.180.122:9191/api/FileManager/api/image/{imageName}";
                //var imageUrl = $"http://adeptinfo.co.in:4329/api/FileManager/api/image/{imageName}";
                //var imageUrl = $"https://api.salasarservices.in/api/FileManager/api/image/{imageName}";
                var imageUrl = $"http://13.200.151.26:8085/api/FileManager/api/image/{imageName}";  //Clint_API

                // Create an HttpClient instance to fetch the image
                using (var httpClient = new HttpClient())
                {
                    var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                    // Convert the byte array to an ImageSource (you could also use StreamImageSource)
                    var imageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(imageBytes));

                    // Set the image to the Image control
                    //imgCam.Source = imageSource;
                    if (imageCapson == "Clear Tag Number Visible Photo")
                    {
                        EditProposalAnimalDetails.FirstImage = imageSource;
                    }
                    else if (imageCapson == "Right Side (Right side of the Animal)")
                    {
                        EditProposalAnimalDetails.SecondImage = imageSource;

                    }
                    else if (imageCapson == "Left Side (Right side of the Animal)")
                    {
                        EditProposalAnimalDetails.ThirdImage = imageSource;

                    }
                    else if (imageCapson == "Front of Animal")
                    {
                       EditProposalAnimalDetails.FourthImage  = imageSource;

                    }
                    else if (imageCapson == "Back side of Animal")
                    {
                        EditProposalAnimalDetails.FivethImage = imageSource;

                    }
                    else if (imageCapson == "Along with the Owner/ Proposer")
                    {
                        EditProposalAnimalDetails.SixthImage = imageSource;

                    }
                    else if (imageCapson == "Veterinary Health Certificate")
                    {
                        EditProposalAnimalDetails.SeventhImage = imageSource;

                    }
                    else if (imageCapson == "Proposal Form")
                    {
                        EditProposalAnimalDetails.EighthImage = imageSource;

                    }
                    else if (imageCapson == "Aadhar card")
                    {
                        EditProposalAnimalDetails.NinthImage = imageSource;

                    }
                    else if (imageCapson == "UTR Image")
                    {
                        EditProposalAnimalDetails.TenthImage = imageSource;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., image not found)
                Console.WriteLine($"Error fetching image: {ex.Message}");
            }
        }

        //UploadPropMethod
        public async void UploadBasicDetailsMethod(string LeadNo)
        {
            UploadAnimalCardLoaderEnable = true;
            string LeadNos = "";
           if (LeadNo !=null)
           {
               LeadNos = LeadNo;
           }
           else 
           {
               LeadNos = Preferences.Get("TempLeadNo", "");
           }
            
            var data = await _animalDetailsDB.GetAnimalProposalByPropId(LeadNos);
            if (data.Count != 0)
            {

                ProposalAnimalCardView.CardAnimalDetailsList.Clear();
                foreach (var item in data)
                {
                    AnimalDataModel animal = new AnimalDataModel()
                    {
                        AnimalDataID = item.AnimalDataID,
                        Propid = item.Propid,
                        LeadNumber = item.LeadNumber,
                        BreedName = item.BreedName,
                        TypeofAnimal = item.TypeofAnimal,
                        Species = item.Species,
                        Category = item.Category,
                        Sex = item.Sex,
                        BodyColour = item.BodyColour,
                        SwitchOfTail = item.SwitchOfTail,
                        MilkYield = item.MilkYield,
                        MilkingStatus = item.MilkingStatus,
                        PregnancyStatus = item.PregnancyStatus,
                        ApproxAgeOfAnimal = item.ApproxAgeOfAnimal,
                        OtherIdentificationMark = item.OtherIdentificationMark,
                        Vaccination = item.Vaccination,
                        RegistrationNo = item.RegistrationNo,
                        DrContactName = item.DrContactName,
                        DrContactNo = item.DrContactNo,
                        HealthCertificateIssueDate = item.HealthCertificateIssueDate,
                        TagNo = item.TagNo,
                        TypeTagging = item.TypeTagging,
                        Ownership = item.Ownership,
                        PaymentRefNo = item.PaymentRefNo,
                        PremiumRate = item.PremiumRate,
                        SumInsured = item.SumInsured,
                        PremiumAmount = item.PremiumAmount
                    };
                    ProposalAnimalCardView.CardAnimalDetailsList.Add(animal);
                }
                UploadAnimalCardLoaderEnable = false;
            }
            else 
            {
               // await App.Current.MainPage.DisplayAlert("Alert! ", "Animal Data not Found", "OK");
                UploadAnimalCardLoaderEnable = false;
            }
        }

        [RelayCommand]
        public async Task UploadAnimalDataViewTapped(AnimalDataModel SelectedAnimalDataItems)
        {
            try
            {
                DataModelPareamiter dataModel = new DataModelPareamiter()
                {
                    LeadNumber = SelectedAnimalDataItems.LeadNumber,
                    Species = SelectedAnimalDataItems.Species,
                    PropId=SelectedAnimalDataItems.Propid
                };
                
                if (dataModel != null)
                {
                    var serializedModel = JsonConvert.SerializeObject(dataModel);
                    var encodedModel = Uri.EscapeDataString(serializedModel);

                    // Pass the serialized and encoded data as a query parameter
                    await Shell.Current.GoToAsync($"//uploadPreviewPage/uploadAnimalCardPage/uploadAnimalDetailsPage?basicAnimalDataKeys={encodedModel}");

                   
                }

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        public async Task TestMethod(int ProposelId,string LeadNumber, string species)
        {
            var specie1 = Convert.ToInt32(species);
            var data = await _animalDetailsDB.GetAnimalProposalByPropIdandSpacies(LeadNumber, species);
            foreach (var item in data)
            {

                // UploadProposalDetailsView.AnimalDataID = item.AnimalDataID;
                UploadProposalDetailsView.PropId = item.Propid.ToString();
                UploadProposalDetailsView.LeadNumber = item.LeadNumber;
                UploadProposalDetailsView.BreedName = item.BreedName;
                UploadProposalDetailsView.CurrentAnimalNumber = item.Species; //opsanal
                UploadProposalDetailsView.SelectedAnimalType = item.TypeofAnimal;
                UploadProposalDetailsView.FirstImage = item.Species;
                UploadProposalDetailsView.SelectedCategoryType = item.Category;
                UploadProposalDetailsView.SelectedSex = item.Sex;
                UploadProposalDetailsView.BodyColour = item.BodyColour;
                UploadProposalDetailsView.SelectedSwitchOfTail = item.SwitchOfTail;
                UploadProposalDetailsView.MilkYield = item.MilkYield;
                UploadProposalDetailsView.MilkingStatus = item.MilkingStatus;
                UploadProposalDetailsView.SelectedPregnancyStatus = item.PregnancyStatus;
                UploadProposalDetailsView.ApproxAgeOfAnimal = item.ApproxAgeOfAnimal;
                UploadProposalDetailsView.OtherIdentificationMark = item.OtherIdentificationMark;
                UploadProposalDetailsView.SelectedVaccination = item.Vaccination;
                UploadProposalDetailsView.RegistrationNumber = item.RegistrationNo;
                UploadProposalDetailsView.DrContactName = item.DrContactName;
                UploadProposalDetailsView.DrContactNo = item.DrContactNo;
                UploadProposalDetailsView.HealthCertificateIssueDate = item.HealthCertificateIssueDate;
                UploadProposalDetailsView.TagNumber = item.TagNo;
                UploadProposalDetailsView.SelectedTypeOfTagging = item.TypeTagging;
                UploadProposalDetailsView.SelectedOwnershipDetails = item.Ownership;
                UploadProposalDetailsView.PaymentUTRNumber = item.PaymentRefNo;
                UploadProposalDetailsView.PremiumRate = item.PremiumRate;
                UploadProposalDetailsView.SumInsured = item.SumInsured;
                UploadProposalDetailsView.PremiumAmount = item.PremiumAmount;

            }
           

            var Spacie = Convert.ToInt32(species);
            Preferences.Set("UploadPageSpacies", Spacie);
            //var Spacie = 1;

            //View Image Throw DB
            var imageList1 = await _proposalImageDB.GetImageDetailsByAnimalId(ProposelId, Spacie);
            var imageList = imageList1.Where(x => x.CompassDegrees == species);
            // var imag = new List<ImageSource>();

            for (int i = 0; i < imageList.Count(); i++)
            {
                if (imageList.ToArray()[i].ImageCapson == "Clear Tag Number Visible Photo")
                {
                    UploadProposalDetailsView.FirstImage = imageList.ToArray()[i].ImgesPath;
                }
                else if (imageList.ToArray()[i].ImageCapson == "Right Side (Right side of the Animal)")
                {
                    UploadProposalDetailsView.SecondImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Left Side (Right side of the Animal)")
                {
                    UploadProposalDetailsView.ThirdImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Front of Animal")
                {
                    UploadProposalDetailsView.FourthImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Back side of Animal")
                {
                    UploadProposalDetailsView.FivethImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Along with the Owner/ Proposer")
                {
                    UploadProposalDetailsView.SixthImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Veterinary Health Certificate")
                {
                    UploadProposalDetailsView.SeventhImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Proposal Form")
                {
                    UploadProposalDetailsView.EighthImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "Aadhar card")
                {
                    UploadProposalDetailsView.NinthImage = imageList.ToArray()[i].ImgesPath;

                }
                else if (imageList.ToArray()[i].ImageCapson == "UTR Image")
                {
                    UploadProposalDetailsView.TenthImage = imageList.ToArray()[i].ImgesPath;
                }


            }
        }

        [RelayCommand]
        public async Task SaveAnimalDetails(object obj)
        {
            var Paramiter = obj;
            AnimalDataModel animalDataModel = new AnimalDataModel();
            try
            {
                //save local in db
                if(Paramiter == "CreateAnimalDetails")
                {
                    animalDataModel.Propid = Convert.ToInt32(PropIds);
                    animalDataModel.LeadNumber = LeadNo;

                    animalDataModel.Breadtype = "Cross";

                    if (AnimalDetails.BreedName != "")
                    {
                        animalDataModel.BreedName = AnimalDetails.BreedName;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter the Breed Name", "ok");
                        //  return;
                    }
                    if (AnimalDetails.SelectedAnimalType != null)
                    {
                        animalDataModel.TypeofAnimal = AnimalDetails.SelectedAnimalType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Type of Animal", "ok");
                        return;
                    }

                    if (AnimalDetails.SelectedCategoryType != null)
                    {
                        animalDataModel.Category = AnimalDetails.SelectedCategoryType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Category", "ok");
                        return;
                    }

                    if (AnimalDetails.SelectedSex != null)
                    {
                        animalDataModel.Sex = AnimalDetails.SelectedSex;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Sex", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.BodyColour))
                    {
                        if (Regex.Match(AnimalDetails.BodyColour, "^[a-zA-Z ]*$").Success)
                        {
                            animalDataModel.BodyColour = AnimalDetails.BodyColour.Trim();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Body Color", "ok");
                            return;
                        }
                    }

                    if (AnimalDetails.SelectedSwitchOfTail != null)
                    {
                        animalDataModel.SwitchOfTail = AnimalDetails.SelectedSwitchOfTail.Trim();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Switch Of Tail Color", "ok");
                        return;
                    }
                    if (AnimalDetails.MilkingStatus != null)
                    {
                        if (AnimalDetails.SelectedSex == "Male")
                        {
                            animalDataModel.MilkYield = "";
                            animalDataModel.MilkingStatus = "";
                        }
                        else
                        {
                            animalDataModel.MilkingStatus = AnimalDetails.MilkingStatus;
                        }
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.MilkYield))
                    {
                        animalDataModel.MilkYield = AnimalDetails.MilkYield;
                    }

                    if (AnimalDetails.SelectedPregnancyStatus != null)
                    {
                        animalDataModel.PregnancyStatus = AnimalDetails.SelectedPregnancyStatus;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.ApproxAgeOfAnimal))
                    {
                        animalDataModel.ApproxAgeOfAnimal = AnimalDetails.ApproxAgeOfAnimal;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.OtherIdentificationMark))
                    {
                        animalDataModel.OtherIdentificationMark = AnimalDetails.OtherIdentificationMark;
                    }

                    if (AnimalDetails.SelectedVaccination != null)
                    {
                        animalDataModel.Vaccination = AnimalDetails.SelectedVaccination;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.RegistrationNumber))
                    {
                        animalDataModel.RegistrationNo = AnimalDetails.RegistrationNumber;
                    }
                    else
                    {
                        animalDataModel.RegistrationNo = "0";
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.DrContactName))
                    {
                        animalDataModel.DrContactName = AnimalDetails.DrContactName;
                    }
                    else
                    {
                        animalDataModel.DrContactName = "0";
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.DrContactNo))
                    {
                        if (Regex.Match(AnimalDetails.DrContactNo, @"^\d{10}$").Success)
                        {
                            animalDataModel.DrContactNo = AnimalDetails.DrContactNo;
                        }
                        else
                        {
                            animalDataModel.DrContactNo = "";
                        }
                    }
                    else
                    {
                        animalDataModel.DrContactNo = "";
                    }

                    if (AnimalDetails.HealthCertificateIssueDate != null)
                    {
                        animalDataModel.HealthCertificateIssueDate = AnimalDetails.HealthCertificateIssueDate;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.TagNumber))
                    {
                        animalDataModel.TagNo = AnimalDetails.TagNumber;
                    }

                    if (AnimalDetails.SelectedTypeOfTagging != null)
                    {
                        animalDataModel.TypeTagging = AnimalDetails.SelectedTypeOfTagging.ToString();
                    }

                    if (AnimalDetails.SelectedOwnershipDetails != null)
                    {
                        animalDataModel.Ownership = AnimalDetails.SelectedOwnershipDetails.ToString();
                    }

                    //PaymentRefNo
                    if (AnimalDetails.PaymentUTRNumber != null)
                    {
                        animalDataModel.PaymentRefNo = AnimalDetails.PaymentUTRNumber;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Payment Ref. No", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.PremiumRate))
                    {
                        animalDataModel.PremiumRate = AnimalDetails.PremiumRate;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Premium Rate", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.SumInsured))
                    {
                        animalDataModel.SumInsured = AnimalDetails.SumInsured;
                    }

                    if (!string.IsNullOrEmpty(AnimalDetails.PremiumAmount))
                    {
                        animalDataModel.PremiumAmount = AnimalDetails.PremiumAmount;
                    }
                    animalDataModel.Species = "";
                    var result = await _animalDetailDB.AddAnimalDetails(animalDataModel);
                    if (result == "Sucessfully Added")
                    {
                        AnimalDetails.AnimalDetailsSection = false;
                        AnimalDetails.ImageSection = true;
                    }
                }
               
                //Save To Server
                else if (Paramiter == "UpdateAnimalDetails")
                {
                    //AnimalDataModel animalDataModel = new AnimalDataModel();
                    //Data save to API
                    animalDataModel.Propid =ProposelId;
                    animalDataModel.Species = PropIds;
                    // animalDataModel.LeadNumber = LeadNo;
                    animalDataModel.AnimalDataID = Convert.ToInt32(PropIds);

                    animalDataModel.Breadtype = "Cross";

                    if (UploadProposalDetailsView.BreedName != "")
                    {
                        animalDataModel.BreedName = UploadProposalDetailsView.BreedName;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter the Breed Name", "ok");
                        return;
                    }
                    if (UploadProposalDetailsView.SelectedAnimalType != null)
                    {
                        animalDataModel.TypeofAnimal = UploadProposalDetailsView.SelectedAnimalType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Type of Animal", "ok");
                        return;
                    }

                    if (UploadProposalDetailsView.SelectedCategoryType != null)
                    {
                        animalDataModel.Category = UploadProposalDetailsView.SelectedCategoryType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Category", "ok");
                        return;
                    }

                    if (UploadProposalDetailsView.SelectedSex != null)
                    {
                        animalDataModel.Sex = UploadProposalDetailsView.SelectedSex;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Sex", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.BodyColour))
                    {
                        if (Regex.Match(UploadProposalDetailsView.BodyColour, "^[a-zA-Z ]*$").Success)
                        {
                            animalDataModel.BodyColour = UploadProposalDetailsView.BodyColour.Trim();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Body Color", "ok");
                            return;
                        }
                    }

                    if (UploadProposalDetailsView.SelectedSwitchOfTail != null)
                    {
                        animalDataModel.SwitchOfTail = UploadProposalDetailsView.SelectedSwitchOfTail.Trim();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Switch Of Tail Color", "ok");
                        return;
                    }
                    if (UploadProposalDetailsView.MilkingStatus != null)
                    {
                        if (UploadProposalDetailsView.SelectedSex == "Male")
                        {
                            animalDataModel.MilkYield = "";
                            animalDataModel.MilkingStatus = "";
                        }
                        else
                        {
                            animalDataModel.MilkingStatus = UploadProposalDetailsView.MilkingStatus;
                        }
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.MilkYield))
                    {
                        animalDataModel.MilkYield = UploadProposalDetailsView.MilkYield;
                    }

                    if (UploadProposalDetailsView.SelectedPregnancyStatus != null)
                    {
                        animalDataModel.PregnancyStatus = UploadProposalDetailsView.SelectedPregnancyStatus;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.ApproxAgeOfAnimal))
                    {
                        animalDataModel.ApproxAgeOfAnimal = UploadProposalDetailsView.ApproxAgeOfAnimal;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.OtherIdentificationMark))
                    {
                        animalDataModel.OtherIdentificationMark = UploadProposalDetailsView.OtherIdentificationMark;
                    }

                    if (UploadProposalDetailsView.SelectedVaccination != null)
                    {
                        animalDataModel.Vaccination = UploadProposalDetailsView.SelectedVaccination;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.RegistrationNumber))
                    {
                        animalDataModel.RegistrationNo = UploadProposalDetailsView.RegistrationNumber;
                    }
                    else
                    {
                        animalDataModel.RegistrationNo = "0";
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.DrContactName))
                    {
                        animalDataModel.DrContactName = UploadProposalDetailsView.DrContactName;
                    }
                    else
                    {
                        animalDataModel.DrContactName = "0";
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.DrContactNo))
                    {
                        if (Regex.Match(UploadProposalDetailsView.DrContactNo, @"^\d{10}$").Success)
                        {
                            animalDataModel.DrContactNo = UploadProposalDetailsView.DrContactNo;
                        }
                        else
                        {
                            animalDataModel.DrContactNo = "";
                        }
                    }
                    else
                    {
                        animalDataModel.DrContactNo = "";
                    }

                    if (UploadProposalDetailsView.HealthCertificateIssueDate != null)
                    {
                        animalDataModel.HealthCertificateIssueDate = UploadProposalDetailsView.HealthCertificateIssueDate;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.TagNumber))
                    {
                        animalDataModel.TagNo = UploadProposalDetailsView.TagNumber;
                    }

                    if (UploadProposalDetailsView.SelectedTypeOfTagging != null)
                    {
                        animalDataModel.TypeTagging = UploadProposalDetailsView.SelectedTypeOfTagging.ToString();
                    }

                    if (UploadProposalDetailsView.SelectedOwnershipDetails != null)
                    {
                        animalDataModel.Ownership = UploadProposalDetailsView.SelectedOwnershipDetails.ToString();
                    }

                    //PaymentRefNo
                    if (UploadProposalDetailsView.PaymentUTRNumber != null)
                    {
                        animalDataModel.PaymentRefNo = UploadProposalDetailsView.PaymentUTRNumber;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Payment Ref. No", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.PremiumRate))
                    {
                        animalDataModel.PremiumRate = UploadProposalDetailsView.PremiumRate;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Premium Rate", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.SumInsured))
                    {
                        animalDataModel.SumInsured = UploadProposalDetailsView.SumInsured;
                    }

                    if (!string.IsNullOrEmpty(UploadProposalDetailsView.PremiumAmount))
                    {
                        animalDataModel.PremiumAmount = UploadProposalDetailsView.PremiumAmount;
                    }
                    

                    //Call API
                    var data = await _uploadAnimalDetailToServer.UpdateAnimalDetailsToServer(animalDataModel);
                    if (data != null)
                    {
                        UploadProposalDetailsView.AnimalDetailsSection = false;
                        UploadProposalDetailsView.ImageSection = true;
                    }
                }

               //Save To Server
                else if (Paramiter == "EditAnimalDetails")
                {
                    AnimalDataModel editAnimalDataModel = new AnimalDataModel();
                    //Data save to API
                    editAnimalDataModel.Propid =ProposelId;
                    editAnimalDataModel.Species = EditSpecise;
                    // animalDataModel.LeadNumber = LeadNo;
                    editAnimalDataModel.AnimalDataID = Convert.ToInt32(EditSpecise);

                    editAnimalDataModel.Breadtype = "Cross";

                    if (EditProposalAnimalDetails.BreedName != "")
                    {
                        editAnimalDataModel.BreedName = EditProposalAnimalDetails.BreedName;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter the Breed Name", "ok");
                        return;
                    }
                    if (EditProposalAnimalDetails.SelectedAnimalType != null)
                    {
                        editAnimalDataModel.TypeofAnimal = EditProposalAnimalDetails.SelectedAnimalType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Type of Animal", "ok");
                        return;
                    }

                    if (EditProposalAnimalDetails.SelectedCategoryType != null)
                    {
                        editAnimalDataModel.Category = EditProposalAnimalDetails.SelectedCategoryType;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Category", "ok");
                        return;
                    }

                    if (EditProposalAnimalDetails.SelectedSex != null)
                    {
                        editAnimalDataModel.Sex = EditProposalAnimalDetails.SelectedSex;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Sex", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.BodyColour))
                    {
                        if (Regex.Match(EditProposalAnimalDetails.BodyColour, "^[a-zA-Z ]*$").Success)
                        {
                            editAnimalDataModel.BodyColour = EditProposalAnimalDetails.BodyColour.Trim();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Body Color", "ok");
                            return;
                        }
                    }

                    if (EditProposalAnimalDetails.SelectedSwitchOfTail != null)
                    {
                        editAnimalDataModel.SwitchOfTail = EditProposalAnimalDetails.SelectedSwitchOfTail.Trim();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Switch Of Tail Color", "ok");
                        return;
                    }
                    if (EditProposalAnimalDetails.MilkingStatus != null)
                    {
                        if (EditProposalAnimalDetails.SelectedSex == "Male")
                        {
                            editAnimalDataModel.MilkYield = "";
                            editAnimalDataModel.MilkingStatus = "";
                        }
                        else
                        {
                            editAnimalDataModel.MilkingStatus = EditProposalAnimalDetails.MilkingStatus;
                        }
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.MilkYield))
                    {
                        editAnimalDataModel.MilkYield = EditProposalAnimalDetails.MilkYield;
                    }

                    if (EditProposalAnimalDetails.SelectedPregnancyStatus != null)
                    {
                        editAnimalDataModel.PregnancyStatus = EditProposalAnimalDetails.SelectedPregnancyStatus;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.ApproxAgeOfAnimal))
                    {
                        editAnimalDataModel.ApproxAgeOfAnimal = EditProposalAnimalDetails.ApproxAgeOfAnimal;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.OtherIdentificationMark))
                    {
                        editAnimalDataModel.OtherIdentificationMark = EditProposalAnimalDetails.OtherIdentificationMark;
                    }

                    if (EditProposalAnimalDetails.SelectedVaccination != null)
                    {
                        editAnimalDataModel.Vaccination = EditProposalAnimalDetails.SelectedVaccination;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.RegistrationNumber))
                    {
                        editAnimalDataModel.RegistrationNo = EditProposalAnimalDetails.RegistrationNumber;
                    }
                    else
                    {
                        editAnimalDataModel.RegistrationNo = "0";
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.DrContactName))
                    {
                        editAnimalDataModel.DrContactName = EditProposalAnimalDetails.DrContactName;
                    }
                    else
                    {
                        editAnimalDataModel.DrContactName = "0";
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.DrContactNo))
                    {
                        if (Regex.Match(EditProposalAnimalDetails.DrContactNo, @"^\d{10}$").Success)
                        {
                            editAnimalDataModel.DrContactNo = EditProposalAnimalDetails.DrContactNo;
                        }
                        else
                        {
                            editAnimalDataModel.DrContactNo = "";
                        }
                    }
                    else
                    {
                        editAnimalDataModel.DrContactNo = "";
                    }

                    if (EditProposalAnimalDetails.HealthCertificateIssueDate != null)
                    {
                        editAnimalDataModel.HealthCertificateIssueDate = EditProposalAnimalDetails.HealthCertificateIssueDate;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.TagNumber))
                    {
                        editAnimalDataModel.TagNo = EditProposalAnimalDetails.TagNumber;
                    }

                    if (EditProposalAnimalDetails.SelectedTypeOfTagging != null)
                    {
                        editAnimalDataModel.TypeTagging = EditProposalAnimalDetails.SelectedTypeOfTagging.ToString();
                    }

                    if (EditProposalAnimalDetails.SelectedOwnershipDetails != null)
                    {
                        editAnimalDataModel.Ownership = EditProposalAnimalDetails.SelectedOwnershipDetails.ToString();
                    }

                    //PaymentRefNo
                    if (EditProposalAnimalDetails.PaymentUTRNumber != null)
                    {
                        editAnimalDataModel.PaymentRefNo = EditProposalAnimalDetails.PaymentUTRNumber;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Payment Ref. No", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.PremiumRate))
                    {
                        editAnimalDataModel.PremiumRate = EditProposalAnimalDetails.PremiumRate;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Premium Rate", "ok");
                        return;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.SumInsured))
                    {
                        editAnimalDataModel.SumInsured = EditProposalAnimalDetails.SumInsured;
                    }

                    if (!string.IsNullOrEmpty(EditProposalAnimalDetails.PremiumAmount))
                    {
                        editAnimalDataModel.PremiumAmount = EditProposalAnimalDetails.PremiumAmount;
                    }
                    

                    //Call API
                    var data = await _editProposalService.EditAnimalDetailsSave(editAnimalDataModel);
                    if (data != null)
                    {
                        EditProposalAnimalDetails.AnimalDetailsSection = false;
                        EditProposalAnimalDetails.ImageSection = true;
                    }
                }
               

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        //Image Save To Server using Upload Page

        [RelayCommand]
        public async void ImageSaveServer()
        {
            var SpaciesValue= Preferences.Get("UploadPageSpacies", 0);
            List<ProposalImages> imageList = new List<ProposalImages>();
            if (SpaciesValue != 0)
            {
                var imageList1 = await _proposalImageDB.GetImageDetailsByAnimalId(ProposelId, SpaciesValue);
                 imageList =  imageList1.Where(x => x.CompassDegrees == SpaciesValue.ToString()).ToList();
                //Remove Prefrence
                Preferences.Remove("UploadPageSpacies");
            }
            else
            {
                var imageList1 = await _proposalImageDB.GetAnimalImage();
                 imageList = imageList1.Where(x => x.CompassDegrees == EditSpecise).ToList();
            }
            
            List<ProposalImages> Images = new List<ProposalImages>();
           
            
            if (SpaciesValue != 0 || EditSpecise != null)
            {
                //var result = await auth.SavePropserDetails(propser);
                if (ProposelId != 0 || LeadNo != null)
                {
                    EditAnimalPageLoaderEnable = true;
                    int propId = ProposelId;
                    if (imageList.Count() > 0)
                    {
                        for (int i = 0; i < imageList.Count(); i++)
                        {
                            //imageList.ToArray()[0].Name

                            Images.Add(new ProposalImages
                            {
                                ImageId = imageList.ToArray()[i].ImageId,
                                PropId = propId,
                                // CreatedDate= imageList.ToArray()[i].CreatedDate,

                                ImgesPath = imageList.ToArray()[i].ImgesPath,
                                ImageName = imageList.ToArray()[i].ImgesPath,
                                CompassDegrees = imageList.ToArray()[i].CompassDegrees,
                                //ImageName = imageList.ToArray()[i].CreatedDate + "_" +imageList.ToArray()[i].ImgesPath,
                                ImageCapson = imageList.ToArray()[i].ImageCapson,
                                Latitude = imageList.ToArray()[i].Latitude,
                                Longitude = imageList.ToArray()[i].Longitude,
                                TimeStamp = imageList.ToArray()[i].TimeStamp
                            });
                        }
                        var returnValueImages = await _uploadAnimalDetailToServer.SaveAnimalImagesDetails(Images);
                        //For Image List Clear
                        Images.Clear();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Proposal data not upload", "Ok");
                        return;
                    }

                }
                else
                {
                    // ProposalId = retrunvalue.ProposalId;
                }

                if (ProposelId != 0)
                {
                    //await DisplayAlert("", "Proposal Details Uploaded", "ok");

                    int uploadimagescount = 0;
                    for (int i = 0; i < imageList.Count(); i++)
                    {
                        var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                        uploadimagescount++;

                        try
                        {
                            //read file into upfilebytes array
                            var fileBytes = File.ReadAllBytes(imageList.ToArray()[i].ImgesPath);

                            var name = Path.GetFileName(imageList.ToArray()[i].ImgesPath);
                            // var newFileName = PropId + "_" +name;
                            var newFileName = ProposelId + "_" + imageList.ToArray()[i].CompassDegrees + "_" + name;
                            //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                            HttpClient client = new HttpClient();
                            MultipartFormDataContent content = new MultipartFormDataContent();
                            ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                            content.Add(byteContent, "File", newFileName);

                            //upload MultipartFormDataContent content async and store response in response var
                            var response = await client.PostAsync(url, content);

                            //read response result as a string async into json var
                            var responsestr = response.Content.ReadAsStringAsync().Result;

                            //debug
                            //Debug.WriteLine(responsestr);
                            if (imageList != null && imageList.Count() == uploadimagescount)
                            {
                                await App.Current.MainPage.DisplayAlert("ProposalId: " + ProposelId, "Proposal Images Uploaded", "ok");

                                GlobalVariables.checkReturnSamepage = 1;
                                if (SpaciesValue != 0 )
                                {
                                    await Shell.Current.GoToAsync($"//uploadPreviewPage/uploadAnimalCardPage");

                                    EditAnimalPageLoaderEnable = false;
                                }
                                else
                                {
                                    if(EditSpecise != null)
                                    {
                                        if (EditProposalAnimalDetails.TestComman == "EditAnimalDetails")
                                        {
                                            await Shell.Current.GoToAsync($"//editPreviewPage/editProposalDetails/editAnimalCardPage");
                                            EditAnimalPageLoaderEnable = false;
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
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error Show", "Proposal Details Not Uploaded", "ok");

                }
            }
        }

        [RelayCommand]
        public async void DirectImageSaveServerByCard(object obj)
        {
            var Paramiter = obj as AnimalDataModel;
            var ProposelId = Paramiter.Propid;
            var SpaciesValue =int.Parse(Paramiter.Species);
            UploadAnimalCardLoaderEnable = true;

            AnimalDataModel animalDataModel = new AnimalDataModel();

            animalDataModel.Propid = Paramiter.Propid;
            animalDataModel.Species = Paramiter.Species;
            animalDataModel.LeadNumber = Paramiter.LeadNumber;
            animalDataModel.AnimalDataID = Paramiter.AnimalDataID;
            animalDataModel.BreedName = Paramiter.BreedName;
            animalDataModel.Breadtype = "Cross";
            animalDataModel.TypeofAnimal = Paramiter.TypeofAnimal;
            animalDataModel.Category = Paramiter.Category;
            animalDataModel.Sex = Paramiter.Sex;
            animalDataModel.BodyColour = Paramiter.BodyColour.Trim();
            animalDataModel.SwitchOfTail = Paramiter.SwitchOfTail;
            animalDataModel.MilkingStatus = Paramiter.MilkingStatus;
            animalDataModel.MilkYield = Paramiter.MilkYield;
            animalDataModel.PregnancyStatus = Paramiter.PregnancyStatus;
            animalDataModel.ApproxAgeOfAnimal = Paramiter.ApproxAgeOfAnimal;
            animalDataModel.OtherIdentificationMark = Paramiter.OtherIdentificationMark;
            animalDataModel.Vaccination = Paramiter.Vaccination;
            animalDataModel.RegistrationNo = Paramiter.RegistrationNo;
            animalDataModel.DrContactName = Paramiter.DrContactName;
            animalDataModel.DrContactNo = Paramiter.DrContactNo;
            animalDataModel.HealthCertificateIssueDate = Paramiter.HealthCertificateIssueDate;
            animalDataModel.TagNo = Paramiter.TagNo;
            animalDataModel.TypeTagging = Paramiter.TypeTagging;
            animalDataModel.Ownership = Paramiter.Ownership;
            animalDataModel.PaymentRefNo = Paramiter.PaymentRefNo;
            animalDataModel.PremiumRate = Paramiter.PremiumRate;
            animalDataModel.SumInsured = Paramiter.SumInsured;
            animalDataModel.PremiumAmount = Paramiter.PremiumAmount;

            //Call API
            var data = await _uploadAnimalDetailToServer.UpdateAnimalDetailsToServer(animalDataModel);
            if (data != null)
            {
               // await App.Current.MainPage.DisplayAlert("Success", "Animal Details data upload", "Ok");
            }
            List<ProposalImages> imageList = new List<ProposalImages>();
            if (SpaciesValue != 0)
            {
                var imageList1 = await _proposalImageDB.GetImageDetailsByAnimalId(ProposelId, SpaciesValue);
                imageList = imageList1.Where(x => x.CompassDegrees == SpaciesValue.ToString()).ToList();
               
            }
            
            List<ProposalImages> Images = new List<ProposalImages>();


            if (SpaciesValue != 0 || EditSpecise != null)
            {
                //var result = await auth.SavePropserDetails(propser);
                if (ProposelId != 0 || LeadNo != null)
                {
                    EditAnimalPageLoaderEnable = true;
                    int propId = ProposelId;
                    if (imageList.Count() > 0)
                    {
                        for (int i = 0; i < imageList.Count(); i++)
                        {
                            //imageList.ToArray()[0].Name

                            Images.Add(new ProposalImages
                            {
                                ImageId = imageList.ToArray()[i].ImageId,
                                PropId = propId,
                                // CreatedDate= imageList.ToArray()[i].CreatedDate,

                                ImgesPath = imageList.ToArray()[i].ImgesPath,
                                ImageName = imageList.ToArray()[i].ImgesPath,
                                CompassDegrees = imageList.ToArray()[i].CompassDegrees,
                                //ImageName = imageList.ToArray()[i].CreatedDate + "_" +imageList.ToArray()[i].ImgesPath,
                                ImageCapson = imageList.ToArray()[i].ImageCapson,
                                Latitude = imageList.ToArray()[i].Latitude,
                                Longitude = imageList.ToArray()[i].Longitude,
                                TimeStamp = imageList.ToArray()[i].TimeStamp

                            });
                        }
                        var returnValueImages = await _uploadAnimalDetailToServer.SaveAnimalImagesDetails(Images);
                        //For Image List Clear
                        Images.Clear();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Proposal data not upload", "Ok");
                        return;
                    }

                }
                else
                {
                    // ProposalId = retrunvalue.ProposalId;
                }

                if (ProposelId != 0)
                {
                    //await DisplayAlert("", "Proposal Details Uploaded", "ok");

                    int uploadimagescount = 0;
                    for (int i = 0; i < imageList.Count(); i++)
                    {
                        var url = GlobalVariables.appUrl + "api/FileManager";// "http://15.207.209.137:8085/api/FileManager";
                        uploadimagescount++;

                        try
                        {
                            //read file into upfilebytes array
                            var fileBytes = File.ReadAllBytes(imageList.ToArray()[i].ImgesPath);

                            var name = Path.GetFileName(imageList.ToArray()[i].ImgesPath);
                            // var newFileName = PropId + "_" +name;
                            var newFileName = ProposelId + "_" + SpaciesValue + "_" + name;
                            //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
                            HttpClient client = new HttpClient();
                            MultipartFormDataContent content = new MultipartFormDataContent();
                            ByteArrayContent byteContent = new ByteArrayContent(fileBytes);
                            content.Add(byteContent, "File", newFileName);

                            //upload MultipartFormDataContent content async and store response in response var
                            var response = await client.PostAsync(url, content);

                            //read response result as a string async into json var
                            var responsestr = response.Content.ReadAsStringAsync().Result;

                            //debug
                            //Debug.WriteLine(responsestr);
                            if (imageList != null && imageList.Count() == uploadimagescount)
                            {
                                await App.Current.MainPage.DisplayAlert("ProposalId: " + ProposelId, "Proposal Images Uploaded", "ok");

                                GlobalVariables.checkReturnSamepage = 1;
                                if (SpaciesValue != 0)
                                {
                                    await Shell.Current.GoToAsync($"//uploadPreviewPage/uploadAnimalCardPage");

                                    UploadAnimalCardLoaderEnable = false;
                                }
                                else
                                {
                                    if (EditSpecise != null)
                                    {
                                        if (EditProposalAnimalDetails.TestComman == "EditAnimalDetails")
                                        {
                                            await Shell.Current.GoToAsync($"//editPreviewPage/editProposalDetails/editAnimalCardPage");
                                            UploadAnimalCardLoaderEnable = false;
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
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error Show", "Proposal Details Not Uploaded", "ok");

                }
            }
        }


        [RelayCommand]
        public async void BackToCardPage()
        {
            try
            {
                await Shell.Current.GoToAsync($"//editPreviewPage");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        [RelayCommand]
        public async Task CameraToggelClicked(object obj)
        {
            try
            {
                ToggelButtonValue = obj.ToString();
                AnimalDetails.CameraToggelBg = Colors.Cyan;
                AnimalDetails.GalleryToggelBg = Colors.Transparent;
                EditProposalAnimalDetails.CameraToggelBg = Colors.Cyan;
                EditProposalAnimalDetails.GalleryToggelBg = Colors.Transparent;
            
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

        }

        [RelayCommand]
        public async Task GalleryToggelClicked(object obj)
        {
            try
            {
                ToggelButtonValue = obj.ToString();
                AnimalDetails.CameraToggelBg = Colors.Transparent;
                AnimalDetails.GalleryToggelBg = Colors.Cyan;
                EditProposalAnimalDetails.CameraToggelBg = Colors.Transparent;
                EditProposalAnimalDetails.GalleryToggelBg = Colors.Cyan;
            
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
                if (ToggelButtonValue == "GallerySelect" && Text!= "Upload PDF File")
                {
                    var photo = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Pick Image Please",
                        FileTypes = FilePickerFileType.Images,

                    });

                    byte[] fileBytes = File.ReadAllBytes(photo.FullPath);
                    stream1 = new MemoryStream(fileBytes);
                }
                else if (Text == "Upload PDF File")
                {
                    var photo = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Pick PDF Files",
                        FileTypes = FilePickerFileType.Pdf,

                    });
                    AnimalDetails.SelectedPDFFileName = photo.FileName;
                    EditProposalAnimalDetails.SelectedPDFFileName = photo.FileName;
                    
                    byte[] fileBytes = File.ReadAllBytes(photo.FullPath);
                    stream1 = new MemoryStream(fileBytes);
                }
                else
                {
                    if(ToggelButtonValue == "CameraSelect" && Text != "Upload PDF File")
                    {
                        var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                        {
                            DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                            Directory = "ClaimIntimation",
                            SaveToAlbum = true
                        });
                        stream1 = photo.GetStream();
                    }
                    else 
                    {
                        await App.Current.MainPage.DisplayAlert("Camera Alert", "Camera App not Supported Contact to IT Team.", "OK");
                    }
                    
                }



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
                            //var NewImageName = aDate.ToString().Replace(" 12:00:00 AM", "") + "001" + ".jpg";
                            //ImageNameModify
                            var NewImageName = DateTime.Now.ToString("dd:MM:yyy:hh:ms").Replace(":", "") + ".jpg";

                            // Save the modified image to local storage
                            var filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), NewImageName);
                            await SaveImageAsync(modifiedImageStream, filePath);

                            var displayStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                            if (Text == "Clear Tag Number Visible Photo")
                            {
                                AnimalDetails.FirstImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.FirstImage = ImageSource.FromStream(() => displayStream);
                            }
                            else if (Text == "Right Side (Right side of the Animal)")
                            {
                                AnimalDetails.SecondImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.SecondImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Left Side (Right side of the Animal)")
                            {
                                AnimalDetails.ThirdImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.ThirdImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Front of Animal")
                            {
                                AnimalDetails.FourthImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.FourthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Back side of Animal")
                            {
                                AnimalDetails.FivethImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.FivethImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Along with the Owner/ Proposer")
                            {
                                AnimalDetails.SixthImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.SixthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Veterinary Health Certificate")
                            {
                                AnimalDetails.SeventhImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.SeventhImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Proposal Form")
                            {
                                AnimalDetails.EighthImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.EighthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Aadhar Card")
                            {
                                AnimalDetails.NinthImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.NinthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "UTR Image")
                            {
                                AnimalDetails.TenthImage = ImageSource.FromStream(() => displayStream);
                                EditProposalAnimalDetails.TenthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Upload PDF File")
                            {
                               // AnimalDetails.SelectedPDFFileName = $"Selected File: {result.FileName}";
                              //  EditProposalAnimalDetails.SelectedPDFFileName = $"Selected File: {result.FileName}";
                                // AnimalDetails.TenthImage = ImageSource.FromStream(() => displayStream);
                                //EditProposalAnimalDetails.TenthImage = ImageSource.FromStream(() => displayStream);

                            }

                            // var id = proposerDB.GetLastSubmitedProposer();
                            //int id = 0;
                            ProposalImages images1 = new ProposalImages();
                            images1.ImgesPath = filePath;
                            //images.PropId = id;

                            images1.ImageName = NewImageName;
                            //images1.CustomerMobileNo = proposer1.CustomerMobile + proposer1.CustomerName;
                            images1.CustomerMobileNo = Preferences.Get("CustomerNameAndMobile", "");

                            //var tdata= Preferences.Get("CattelflagCount", 0);
                           // images1.PropId = Convert.ToInt32(PropIds);  //Save throw parameter
                            if(EditSpecise !=null && EditSpecise != "")
                            {
                                images1.PropId = Convert.ToInt32(ProposelId);
                                images1.CompassDegrees = EditSpecise;
                                images1.CreatedDate = ""; //tdata.ToString();
                            }
                            else
                            {
                                images1.PropId = Convert.ToInt32(PropIds);
                                images1.CreatedDate = ""; //tdata.ToString();
                                images1.CompassDegrees = "";
                            }
                            if (location != null)
                            {
                                images1.Latitude = location.Latitude.ToString();
                                images1.Longitude = location.Longitude.ToString();

                            }
                            else
                            {
                                images1.Latitude = "";
                                images1.Longitude = "";
                            }
                            images1.ImageCapson = Text;
                            images1.TimeStamp = aDate.ToString("dddd, dd MMMM yyyy HH:mm:ss");

                            var retrunvalue = await _proposalImageDB.AddAnimalImage(images1);
                            if (retrunvalue == "Sucessfully Added")
                            {
                                await App.Current.MainPage.DisplayAlert("Image Add", retrunvalue, "OK");
                                //this is for obj value delete

                            }
                            else if (retrunvalue == "Already Exist")
                            {
                                await App.Current.MainPage.DisplayAlert("Previous image replace sucessfully", retrunvalue, "OK");
                            }

                            Preferences.Clear("CattelflagCount");


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
        public async Task SaveAnimalImageDetails()
        {
            try
            {
                var NoOfInsuredCattle = Convert.ToInt32(NoOfCattleToBeInsured);
                var Propid = Convert.ToInt32(PropIds);
                var SpaicesValue = Convert.ToString(Cattelflag);
                var mobileNum = Preferences.Get("CustomerNameAndMobile", ""); 
                var data1 =await _proposalImageDB.UpdateImageDetailsByCatelFlag(Cattelflag, Propid);


                //update Species Local
                var result =await _animalDetailDB.updateAnimalProposalSpecies(Propid, SpaicesValue);
                if (NoOfInsuredCattle == Cattelflag)
                {
                    await App.Current.MainPage.DisplayAlert("Record Saved", "Animal Details Save Succecfully!", "ok");

                    AnimalDetails.AnimalDetailsSection = true;
                    AnimalDetails.ImageSection = false;
                
                    //Reset Data
                    Cattelflag = 1;
                    await Shell.Current.GoToAsync("///homePage");
                    ClearFields();

                }
                else
                {
                    AnimalDetails.AnimalDetailsSection = true;
                    AnimalDetails.ImageSection = false;
                    Cattelflag = Cattelflag + 1;
                    AnimalDetails.CurrentAnimalNumber = Cattelflag.ToString();
                    ClearFields();
                }
                // ClearFields();
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public void ClearFields()
        {
            AnimalDetails.SelectedAnimalType=null;
            AnimalDetails.SelectedCategoryType= null;
            AnimalDetails.SelectedSwitchOfTail= null;
            AnimalDetails.BodyColour="";
            AnimalDetails.ApproxAgeOfAnimal="";
            AnimalDetails.MilkingStatus="";
            AnimalDetails.MilkYield="";
            AnimalDetails.SelectedPregnancyStatus = null;
            AnimalDetails.SelectedVaccination= null;
            AnimalDetails.OtherIdentificationMark= "";
            AnimalDetails.DrContactName="";
            AnimalDetails.RegistrationNumber="";
            AnimalDetails.HealthCertificateIssueDate="";
            AnimalDetails.SelectedTypeOfTagging=null;
            AnimalDetails.SelectedOwnershipDetails=null;
            AnimalDetails.TagNumber="";
            AnimalDetails.PaymentUTRNumber="";
            AnimalDetails.PremiumRate="";
            AnimalDetails.SumInsured="";
            AnimalDetails.PremiumAmount="";
            AnimalDetails.FarmerPremiumPercentage="";
            AnimalDetails.GovPremiumPercentage="";
            AnimalDetails.CentralSharePercentage = "";

            AnimalDetails.FirstImage = "";
            AnimalDetails.SecondImage = "";
            AnimalDetails.ThirdImage = "";
            AnimalDetails.FourthImage = "";
            AnimalDetails.FirstImage = "";
            AnimalDetails.SixthImage = "";
            AnimalDetails.SeventhImage = "";
            AnimalDetails.EighthImage = "";
            AnimalDetails.NinthImage = "";
            AnimalDetails.ThirdImage = "";
        }

    }
}
