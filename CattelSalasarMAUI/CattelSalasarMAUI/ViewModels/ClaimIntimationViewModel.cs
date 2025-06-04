using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CattelSalasarMAUI.SQLiteDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class ClaimIntimationViewModel : BaseViewModel
    {
        
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

        private string _searchLeadNumber;
        public string SearchLeadNumber
        {
            get => _searchLeadNumber;
            set
            {
                if (_searchLeadNumber != value)
                {
                    _searchLeadNumber = value;
                    OnPropertyChanged(nameof(SearchLeadNumber));
                }
            }
        }

        private DateTime _dateOfDeath;
        public DateTime DateOfDeath
        {
            get => _dateOfDeath;
            set
            {
                if (_dateOfDeath != value)
                {
                    _dateOfDeath = value;
                    OnPropertyChanged(nameof(DateOfDeath));
                }
            }
        }

        private DateTime _policyDate;
        public DateTime PolicyDate
        {
            get => _policyDate;
            set
            {
                if (_policyDate != value)
                {
                    _policyDate = value;
                    OnPropertyChanged(nameof(PolicyDate));
                }
            }
        }

        private TimeSpan _timeOfDeath;
        public TimeSpan TimeOfDeath
        {
            get => _timeOfDeath;
            set
            {
                if (_timeOfDeath != value)
                {
                    _timeOfDeath = value;
                    OnPropertyChanged(nameof(TimeOfDeath));
                }
            }
        }
       
        private string _causeOfDeath;
        public string CauseOfDeath
        {
            get => _causeOfDeath;
            set
            {
                if (_causeOfDeath != value)
                {
                    _causeOfDeath = value;
                    OnPropertyChanged(nameof(CauseOfDeath));
                }
            }
        }

        private string _policyNumber;
        public string PolicyNumber
        {
            get => _policyNumber;
            set
            {
                if (_policyNumber != value)
                {
                    _policyNumber = value;
                    OnPropertyChanged(nameof(PolicyNumber));
                }
            }
        }
        
        private string _beneficiaryName;
        public string BeneficiaryName
        {
            get => _beneficiaryName;
            set
            {
                if (_beneficiaryName != value)
                {
                    _beneficiaryName = value;
                    OnPropertyChanged(nameof(BeneficiaryName));
                }
            }
        }
       
        private string _beneficiaryMobileNo;
        public string BeneficiaryMobileNo
        {
            get => _beneficiaryMobileNo;
            set
            {
                if (_beneficiaryMobileNo != value)
                {
                    _beneficiaryMobileNo = value;
                    OnPropertyChanged(nameof(BeneficiaryMobileNo));
                }
            }
        }
        
        private string _customerBankName;
        public string CustomerBankName
        {
            get => _customerBankName;
            set
            {
                if (_customerBankName != value)
                {
                    _customerBankName = value;
                    OnPropertyChanged(nameof(CustomerBankName));
                }
            }
        }
       
        private string _customerAccountNumber;
        public string CustomerAccountNumber
        {
            get => _customerAccountNumber;
            set
            {
                if (_customerAccountNumber != value)
                {
                    _customerAccountNumber = value;
                    OnPropertyChanged(nameof(CustomerAccountNumber));
                }
            }
        }
        
        private string _customerIFSCCode;
        public string CustomerIFSCCode
        {
            get => _customerIFSCCode;
            set
            {
                if (_customerIFSCCode != value)
                {
                    _customerIFSCCode = value;
                    OnPropertyChanged(nameof(CustomerIFSCCode));
                }
            }
        }
        
        private string _aadharNumber;
        public string AadharNumber
        {
            get => _aadharNumber;
            set
            {
                if (_aadharNumber != value)
                {
                    _aadharNumber = value;
                    OnPropertyChanged(nameof(AadharNumber));
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

        [ObservableProperty]
        private bool _claimIntimationSection;
        [ObservableProperty]
        private bool _imageUploadSection;
        [ObservableProperty]
        private bool _claimPageLoader;

        ClaimIntimationBasicDetailsDB _claimIntimationDB = new ClaimIntimationBasicDetailsDB();
        private IClaimIntimationService _claimIntimationService { get; set; }
       
        public ClaimIntimationViewModel(IClaimIntimationService claimIntimationService)
        {
            _claimIntimationService = claimIntimationService;
            ClaimIntimationSection = true;
            ClaimPageLoader = false;
            ImageUploadSection = false;

            GetClaimIntimation();
        }

        public async Task GetClaimIntimation()
        {
            try
            {
                var cardLeadNo = Preferences.Get("IntimationTappedLeadNo", "");
                var cardMobileNo = Preferences.Get("IntimationTappedMobileNo", "");
                var cattleNo = "0";

                var item = await _claimIntimationService.GetBasicClaimIntimationByLeadId(cardLeadNo, cattleNo);

                if (item?.ProposerData != null)
                {
                    DateOfDeath = SafeParseDate(item.ProposerData.DateOfDeath);
                    TimeOfDeath = SafeParseTimeSpan(item.ProposerData.TimeOfDeath);
                    CauseOfDeath = item.ProposerData.CauseOfDeath?.Trim() ?? "";
                    PolicyNumber = item.ProposerData.PolicyNo?.Trim() ?? "";
                    PolicyDate = SafeParseDate(item.ProposerData.SurveyDate);
                    LeadNumber = item.ProposerData.LeadNumber?.Trim() ?? "";
                    BeneficiaryName = item.ProposerData.CustomerName?.Trim() ?? "";
                    BeneficiaryMobileNo = item.ProposerData.CustomerMobile?.Trim() ?? "";
                    CustomerBankName = item.ProposerData.NameOfBank?.Trim() ?? "";
                    AadharNumber = item.ProposerData.AadharNumber?.Trim() ?? "";
                    TagNumber = Preferences.Get("selectedCattleTagNoTapped", "");
                }
                else
                {
                    Console.WriteLine("ProposerData is null");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetClaimIntimation: {ex.Message}");
            }
        }

        // ✅ Helper method to safely parse dates
        private DateTime SafeParseDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return DateTime.MinValue;

            if (DateTime.TryParse(input.Trim(), out var parsedDate))
                return parsedDate;

            Console.WriteLine($"Invalid date format: {input}");
            return DateTime.MinValue; // Default fallback
        }

        // ✅ Helper method to safely parse time spans
        private TimeSpan SafeParseTimeSpan(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return TimeSpan.Zero;

            try
            {
                if (TimeSpan.TryParse(input.Trim(), out var parsedTime))
                    return parsedTime;

                Console.WriteLine($"Invalid time format: {input}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing time span: {ex.Message}");
            }

            return TimeSpan.Zero; // Default fallback
        }


        //public async Task GetClaimIntimation()
        //{
        //    try
        //    {
        //        var cardLeadNo = Preferences.Get("IntimationTappedLeadNo", "");
        //        var cardMobileNo = Preferences.Get("IntimationTappedMobileNo", "");
        //        var cattleNo = "0";

        //        var item = await _claimIntimationService.GetBasicClaimIntimationByLeadId(cardLeadNo, cattleNo);
        //        if (item?.ProposerData != null)
        //        {
        //            //DateOfDeath = DateTime.TryParse(item.ProposerData.DateOfDeath?.Replace("?", "").Trim(), out var deathDate)
        //            //    ? deathDate
        //            //    : DateTime.Now;

        //            DateOfDeath = DateTime.TryParse(item.ProposerData.DateOfDeath?.Trim(), out var deathDate)
        //            ? deathDate
        //            : DateTime.MinValue;

        //            //TimeOfDeath = TimeSpan.TryParse(item.ProposerData.TimeOfDeath, out var deathTime)
        //            //    ? deathTime
        //            //    : TimeSpan.FromMinutes(0);
        //            TimeOfDeath = !string.IsNullOrEmpty(item.ProposerData.TimeOfDeath) &&
        //            TimeSpan.TryParse(item.ProposerData.TimeOfDeath.Trim(), out var deathTime)
        //            ? deathTime
        //            : TimeSpan.Zero; // Safer fallback value


        //            //CauseOfDeath = item.ProposerData.CauseOfDeath?.Trim() ?? "";
        //            CauseOfDeath = (DateTime.TryParse(item.ProposerData.CauseOfDeath?.Trim(), out var causeOfDeath)
        //            ? causeOfDeath
        //            : DateTime.MinValue).ToString();
        //            PolicyNumber = item.ProposerData.PolicyNo?.Trim() ?? "";
        //            PolicyDate = DateTime.TryParse(item.ProposerData.SurveyDate?.Trim(), out var policyDate)
        //                ? policyDate
        //                : DateTime.MinValue;

        //            LeadNumber = item.ProposerData.LeadNumber?.Trim() ?? "";
        //            BeneficiaryName = item.ProposerData.CustomerName?.Trim() ?? "";
        //            BeneficiaryMobileNo = item.ProposerData.CustomerMobile?.Trim() ?? "";
        //            CustomerBankName = item.ProposerData.NameOfBank?.Trim() ?? "";
        //            AadharNumber = item.ProposerData.AadharNumber?.Trim() ?? "";
        //            TagNumber = Preferences.Get("selectedCattleTagNoTapped", "");
        //        }
        //        else
        //        {
        //            Console.WriteLine("ProposerData is null");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error in GetClaimIntimation: {ex.Message}");
        //    }
        //}

        [RelayCommand]
        public async Task SaveAnimalDetails()
        {
            ClaimIntimationModel claimDetails = new ClaimIntimationModel();

            if (LeadNumber.Trim() != null)
            {
                claimDetails.LeadNumber = LeadNumber;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Select Date Of Death", "ok");
                return;
            }
            if (DateOfDeath.ToString() != null)
            {
                claimDetails.DateOfDeath = DateOfDeath.Date.ToString();
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Select Date Of Death", "ok");
                return;
            }

            if (TimeOfDeath.ToString() != null)
            {
                claimDetails.TimeOfDeath = TimeOfDeath.ToString();
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Select Time Of Death", "ok");
                return;
            }

            if (CauseOfDeath != null)
            {
                claimDetails.CauseOfDeath = CauseOfDeath;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Cause Of Death", "ok");
                return;
            }

            if (TagNumber != null)
            {
                claimDetails.CustomerPanCard = TagNumber;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter TagNumber", "ok");
                return;
            }

            if (PolicyNumber != null)
            {
                claimDetails.PolicyNumber = PolicyNumber;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Policy Number", "ok");
                return;
            }

            if (PolicyDate.ToString() != null)
            {
                claimDetails.PolicyDate = PolicyDate.ToString("dd/MM/yyyy");
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Select Policy Date", "ok");
                return;
            }
            //Add BeneficiaryName
            if (BeneficiaryName != null)
            {
                claimDetails.BeneficiaryName = BeneficiaryName;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter  Beneficiary Name", "ok");
                return;
            }
            //Add BeneficiaryMobileNo
            if (BeneficiaryMobileNo != null && Regex.Match(BeneficiaryMobileNo, @"^\d{10}$").Success)
            {
                claimDetails.BeneficiaryMobileNo = BeneficiaryMobileNo;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Beneficiary MobileNNumber", "ok");
                return;
            }
            if (CustomerBankName != null)
            {
                claimDetails.CustomerBankName = CustomerBankName;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Customer Bank Name", "ok");
                return;
            }

            if (CustomerIFSCCode != null)
            {
                claimDetails.CustomerIFSCCode = CustomerIFSCCode;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Customer IFSC Code", "ok");
                return;
            }

            if (CustomerAccountNumber != null)
            {
                claimDetails.CustomerAccountNumber = CustomerAccountNumber;
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Enter Customer Account Number", "ok");
                return;
            }
            //AadharCardNo
            if (AadharNumber != null)
            {
                claimDetails.AadharNumber = AadharNumber;
                claimDetails.SeqNo= Preferences.Get("emaiId", "");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Enter Customer Account Number", "ok");
                return;
            }

            ClaimPageLoader=true;
            var retrunvalue =await _claimIntimationDB.AddClaimIntimationDetails(claimDetails);    // proposerDB.AddProposerDetails(proposer);
            var retrunvalue1 =await _claimIntimationService.SaveClaimIntimationDetailsOnServer(claimDetails);

            if (retrunvalue1.PropId != null)
            {
               await App.Current.MainPage.DisplayAlert("Claim Intimation Details Add Successfully", retrunvalue, "OK");

                ClaimIntimationSection = false;
                ImageUploadSection = true;

                try
                {

                    ClaimIntimationResponceModel data = new ClaimIntimationResponceModel
                    {
                        PropId = retrunvalue1.PropId,
                        LeadNumber = retrunvalue1.LeadNumber,
                        QuoteId = retrunvalue1.QuoteId,  //TagNo Data
                    };
                    var serializedModel = JsonConvert.SerializeObject(data);
                    var encodedModel = Uri.EscapeDataString(serializedModel);
                    await Shell.Current.GoToAsync($"///cardClaimIntimation/claimAnimalCardPage/claimIntimationImagePage?claimAnimalImageKey={encodedModel}");
                  
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }


            }
            else if (retrunvalue == "Already Exist")
            {
             await App.Current.MainPage.DisplayAlert("Claim Intimation Details Updated Successfully", "", "OK");

               
            }

        }

        [RelayCommand]
        public async Task BackPage()
        {
            await Shell.Current.GoToAsync($"///cardClaimIntimation/claimAnimalCardPage");
        }


    }
}
