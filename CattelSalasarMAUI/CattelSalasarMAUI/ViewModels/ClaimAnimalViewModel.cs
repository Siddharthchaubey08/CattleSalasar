using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.Media;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CattelSalasarMAUI.SQLiteDB;
using System.Reflection.Metadata;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class ClaimAnimalViewModel : BaseViewModel, IQueryAttributable
    {
        private bool _claimCardPageLoader;
        public bool ClaimCardPageLoader
        {
            get => _claimCardPageLoader;
            set
            {
                if (_claimCardPageLoader != value)
                {
                    _claimCardPageLoader = value;
                    OnPropertyChanged(nameof(ClaimCardPageLoader));
                }
            }
        }

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

        private AnimalDataCardClass _proposalAnimalCardView;
        public AnimalDataCardClass ProposalAnimalCardView
        {
            get => _proposalAnimalCardView;
            set
            {
                if (_proposalAnimalCardView != value)
                {
                    _proposalAnimalCardView = value;
                    OnPropertyChanged(nameof(ProposalAnimalCardView));
                }
            }
        }
        private AnimalDataCardClass _claimAnimalCardView;
        public AnimalDataCardClass ClaimAnimalCardView
        {
            get => _claimAnimalCardView;
            set
            {
                if (_claimAnimalCardView != value)
                {
                    _claimAnimalCardView = value;
                    OnPropertyChanged(nameof(ClaimAnimalCardView));
                }
            }
        }

        private ClaimAnimalImageCardClass _claimAnimalImageCardView;
        public ClaimAnimalImageCardClass ClaimAnimalImageCardView
        {
            get => _claimAnimalImageCardView;
            set
            {
                if (_claimAnimalImageCardView != value)
                {
                    _claimAnimalImageCardView = value;
                    OnPropertyChanged(nameof(ClaimAnimalImageCardView));
                }
            }
        }

        private bool _claimAnimalCardVisible;
        public bool ClaimAnimalCardVisible
        {
            get => _claimAnimalCardVisible;
            set
            {
                if (_claimAnimalCardVisible != value)
                {
                    _claimAnimalCardVisible = value;
                    OnPropertyChanged(nameof(ClaimAnimalCardVisible));
                }
            }
        }

        private bool _claimAnimalImageVisible;
        public bool ClaimAnimalImageVisible
        {
            get => _claimAnimalCardVisible;
            set
            {
                if (_claimAnimalCardVisible != value)
                {
                    _claimAnimalCardVisible = value;
                    OnPropertyChanged(nameof(ClaimAnimalImageVisible));
                }
            }
        }

        private IEditProposalService _editProposalService { get; set; }
        private IClaimIntimationService _claimIntimationService { get; set; }
        ClaimImageDB _claimImageDB = new ClaimImageDB();
        public ClaimAnimalViewModel(IEditProposalService editProposalService, IClaimIntimationService claimIntimationServic)
        {
             _editProposalService= editProposalService;
            _claimIntimationService = claimIntimationServic;
            ClaimAnimalCardView = new AnimalDataCardClass();
            ClaimAnimalImageCardView = new ClaimAnimalImageCardClass();
            
            ClaimAnimalGridDataView();
            CameraToggelClicked("CameraSelect");
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("claimAnimalCardKey"))
            {
                var serializedModel = query["claimAnimalCardKey"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    var decodedModel = Uri.UnescapeDataString(serializedModel);
                    var dataModel = JsonConvert.DeserializeObject<ClaimIntimationResponceModel>(decodedModel);

                    if (dataModel != null)
                    {
                       var LeadNo = dataModel.LeadNumber;
                       //Preferences.Set("TempLeadNo", );
                        Preferences.Set("IntimationTappedLeadNo", LeadNo);
                       

                        var ClaimPropId= dataModel.PropId;
                        Preferences.Set("IntimationTappedProposalNo", ClaimPropId);
                       // Preferences.Set("TempClaimPropId", ClaimPropId);

                        // Call TestMethod with the deserialized values
                        ClaimAnimalCardVisible = true;
                        ClaimAnimalImageVisible = false;
                        ClaimAnimalImageCardView.LocalSaveButtomVisible = true;
                        ClaimAnimalImageCardView.ImageSaveToServerVisible = false;
                        await ClaimAnimalGridDataView();
                    }

                }

            }
            if (query.ContainsKey("claimAnimalImageKey"))
            {
                var serializedModel = query["claimAnimalImageKey"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    var decodedModel = Uri.UnescapeDataString(serializedModel);
                    var dataModel = JsonConvert.DeserializeObject<ClaimIntimationResponceModel>(decodedModel);

                    if (dataModel != null)
                    {
                       var LeadNumber = dataModel.LeadNumber;
                        var ClaimProposalId = dataModel.PropId;
                        var TagNumber = dataModel.QuoteId;
                        


                        Preferences.Set("TempImgClaimPropId", ClaimProposalId);
                        Preferences.Set("TempImgLeadNumber", LeadNumber);
                        Preferences.Set("TempImgTagNumber", TagNumber);

                        // Call TestMethod with the deserialized values
                        ClaimAnimalCardVisible = true;
                        ClaimAnimalImageVisible = false;

                        ClaimAnimalImageData(LeadNumber, ClaimProposalId, TagNumber);
                    }
                }
            }
        }

        public async Task ClaimAnimalGridDataView()
        {
            ClaimAnimalImageCardView.LocalSaveButtomVisible = true;
            ClaimAnimalImageCardView.ImageSaveToServerVisible = false;
            ClaimAnimalCardVisible = true;
            ClaimAnimalImageVisible = false;
            var LeadNo = Preferences.Get("IntimationTappedLeadNo", "");
           // var ProposalId = Preferences.Get("IntimationTappedProposalNo", "");
            var ProposalId = Preferences.Get("IntimationTappedProposalNo", "");
            if (!string.IsNullOrEmpty(ProposalId))
            {
                ClaimCardPageLoader = false;
                var data = await _claimIntimationService.GetClaimAnimalCardList(ProposalId);
                if (data != null)
                {
                    ClaimAnimalCardView.ClaimCardAnimalDetailsList.Clear();
                    foreach (var item in data)
                    {
                        ClaimAnimalCard animal = new ClaimAnimalCard()
                        {
                            AnimalDataID = item.AnimalDataID,
                            Propid = item.Propid,
                            // LeadNumber = item.,
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
                        ClaimAnimalCardView.ClaimCardAnimalDetailsList.Add(animal);
                    }
                }
                else
                {
                    //await App.Current.MainPage.DisplayAlert("Message ", "Animal Data not Available", "OK");
                }
            }
        }

        [RelayCommand]
        public async Task ClaimAnimalTapped(object obj)
        {
            var paramiter = obj as ClaimAnimalCard;
            try
            {
                
                Preferences.Set("selectedCattleTagNoTapped",paramiter.TagNo);
                await Shell.Current.GoToAsync($"//cardClaimIntimation/claimAnimalCardPage/claimIntimationPage");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            Preferences.Set("TempTypeofAnima", paramiter.TypeofAnimal);

        }

       
        public async Task ClaimAnimalImageData(string LeadNumber, string ClaimProposalId, string TagNumber)
        {
            try
            {
                ClaimAnimalImageCardView.LeadNumber = LeadNumber;
                ClaimAnimalImageCardView.ClaimProposalId = ClaimProposalId;
                ClaimAnimalImageCardView.TagNumber = TagNumber;
                ClaimAnimalImageCardView.TypeOfAnimal = Preferences.Get("TempTypeofAnima","");

                
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
                ClaimAnimalImageCardView.CameraToggelBg = Colors.Cyan;
                ClaimAnimalImageCardView.GalleryToggelBg = Colors.Transparent;

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
                ClaimAnimalImageCardView.CameraToggelBg = Colors.Transparent;
                ClaimAnimalImageCardView.GalleryToggelBg = Colors.Cyan;

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        [RelayCommand]
        public async Task PhotoCapture(object obj)
        {
            var paramiter = obj.ToString();
            var Text = obj.ToString();
            Stream stream1 = null;
            var location = await Geolocation.GetLastKnownLocationAsync();
            try
            {
                if (ToggelButtonValue == "GallerySelect")
                {
                    var photo = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Pick Image Please",
                        FileTypes = FilePickerFileType.Images,

                    });

                    byte[] fileBytes = File.ReadAllBytes(photo.FullPath);
                    stream1 = new MemoryStream(fileBytes);
                }
                else
                {
                    if (ToggelButtonValue == "CameraSelect")
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

                            // Reset the stream position before displaying the image
                            //modifiedImageStream.Position = 0;
                            //ImageTest = ImageSource.FromStream(() => new MemoryStream(modifiedImageStream.ToArray()));

                            var displayStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                            if (Text == "Clear Tag Number Visible Photo")
                            {
                                
                                ClaimAnimalImageCardView.FirstImage = ImageSource.FromStream(() => displayStream);
                            }
                            else if (Text == "Right Side (Right side of the Animal)")
                            {
                                
                                ClaimAnimalImageCardView.SecondImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Left Side (Right side of the Animal)")
                            {
                                
                                ClaimAnimalImageCardView.ThirdImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Front of Animal")
                            {
                               
                                ClaimAnimalImageCardView.FourthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Back side of Animal")
                            {
                               
                                ClaimAnimalImageCardView.FirstImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Along with the Owner/ Proposer")
                            {
                                
                                ClaimAnimalImageCardView.SixthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Veterinary Health Certificate")
                            {
                                
                                ClaimAnimalImageCardView.SeventhImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Proposal Form")
                            {
                                
                                ClaimAnimalImageCardView.EighthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Aadhar Card")
                            {
                                
                                ClaimAnimalImageCardView.NinthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "UTR Image")
                            {
                                
                                ClaimAnimalImageCardView.TenthImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Post-Mortem Report")
                            {
                                
                                ClaimAnimalImageCardView.EleventhImage = ImageSource.FromStream(() => displayStream);

                            }
                            else if (Text == "Beneficiary Bank Passbook")
                            {
                                
                                ClaimAnimalImageCardView.TwelthImage = ImageSource.FromStream(() => displayStream);

                            }

                            ClaimIntimationImageModel images1 = new ClaimIntimationImageModel();
                            images1.ImgesPath = filePath;

                            images1.ImageName = NewImageName;
                           // images1.CustomerMobileNo = Preferences.Get("CustomerNameAndMobile", "");

                            if (images1.ClaimIntimationId != null)
                            {
                                images1.ClaimIntimationId =Convert.ToInt32(Preferences.Get("TempImgClaimPropId", ""));
                                images1.CompassDegrees = "";
                                images1.CreatedDate = ""; //tdata.ToString();
                            }
                            else
                            {
                                images1.ClaimIntimationId = 0;
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
                            
                            images1.LeadNumber = Preferences.Get("TempImgLeadNumber", ""); 
                            images1.ClaimProposalId = Preferences.Get("TempImgClaimPropId", "");
                            images1.TagNumber=Preferences.Get("TempImgTagNumber", "");
                            images1.TypeOfAnimal=Preferences.Get("TempTypeofAnima", "");
                            images1.ImageCapson = Text;
                            images1.TimeStamp = aDate.ToString("dddd, dd MMMM yyyy HH:mm:ss");

                            var retrunvalue = await _claimImageDB.AddClaimIntimationImage(images1);
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
        public async Task SaveAnimalImageDetails(object obj)
        {
            await App.Current.MainPage.DisplayAlert("Sucessfully", "Image Saveed!", "OK");
            ClaimAnimalCardVisible = true;
            ClaimAnimalImageVisible = false;
            await Shell.Current.GoToAsync($"///cardClaimIntimation/claimAnimalCardPage");
            // await ClaimAnimalGridDataView();
        }

        [RelayCommand]
        public async Task BackPage()
        {
            try
            {
                await Shell.Current.GoToAsync($"///cardClaimIntimation/claimAnimalCardPage");

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
           
            
        }
    }
}
