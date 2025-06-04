using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace CattelSalasarMAUI.ViewModels
{
    public partial class ProposalDetailsViewModel : BaseViewModel , IQueryAttributable
    {
        //[ObservableProperty]
        //ProposalDetailsProp _proposalBasicDetails;
        [ObservableProperty]
        ProposalDetailsProp _uploadBasicDetails;
        
        //For Edit Page
        private DataCardPropClass _editDataCardView;
        public DataCardPropClass EditDataCardView
        {
            get => _editDataCardView;
            set
            {
                if (_editDataCardView != value)
                {
                    _editDataCardView = value;
                    OnPropertyChanged(nameof(EditDataCardView));
                }
            }
        }

        private ProposalDetailsProp _ProposalBasicDetails;
        public ProposalDetailsProp ProposalBasicDetails
        {
            get => _ProposalBasicDetails;
            set
            {
                if (_ProposalBasicDetails != value)
                {
                    _ProposalBasicDetails = value;
                    OnPropertyChanged(nameof(ProposalBasicDetails));
                }
            }
        }
        
        private ProposalDetailsProp _editproposalBasicDetails;
        public ProposalDetailsProp EditProposalBasicDetails
        {
            get => _editproposalBasicDetails;
            set
            {
                if (_editproposalBasicDetails != value)
                {
                    _editproposalBasicDetails = value;
                    OnPropertyChanged(nameof(EditProposalBasicDetails));
                }
            }
        }

        [ObservableProperty]
        bool _pageLoaderEnable;
        
        [ObservableProperty]
        int _editProposalId;
        
        [ObservableProperty]
        string _editLeadNumber;

       [ObservableProperty]
        string _loginUserName;
       
        //[ObservableProperty]
        //string _proposalDate;
        
        [ObservableProperty]
        DateTime _proposalDate;

        private bool _editProposalPageLoaderEnable;
        public bool EditProposalPageLoaderEnable
        {
            get => _editProposalPageLoaderEnable;
            set
            {
                if (_editProposalPageLoaderEnable != value)
                {
                    _editProposalPageLoaderEnable = value;
                    OnPropertyChanged(nameof(EditProposalPageLoaderEnable));
                }
            }
        }
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
                    District(ProposalBasicDetails.SelectedState.StateName);
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
                    BlockMethod(ProposalBasicDetails.SelectedDistrict.DistrictName);
                }
            }
        }
        private GetAllSchemeModel _selectedScheme;
        public GetAllSchemeModel SelectedScheme
        {
            get => _selectedScheme;
            set
            {
                if (_selectedScheme != value)
                {
                    _selectedScheme = value;
                    OnPropertyChanged(nameof(SelectedScheme));
                    GetAllSchemeMethod(ProposalBasicDetails.SelectedScheme.SchemeID);
                }
            }
        }
        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                    //CalculateAge(ProposalBasicDetails.DateOfBirth);
                }
            }
        }

        private IBaseDataService _baseDataService {  get; set; }
        private ICreateProposalService _createProposalService {  get; set; }
        private IEditProposalService _editProposalService { get; set; }
        public ProposalDetailsViewModel(IBaseDataService baseDataService, ICreateProposalService createProposalService, IEditProposalService editProposalService)
        {
            _baseDataService = baseDataService;
            _createProposalService = createProposalService;
            _editProposalService = editProposalService;
            //  ProposalBasicDetails = new ProposalDetailsProp();
            UploadBasicDetails = new ProposalDetailsProp();
            EditDataCardView = new DataCardPropClass();

            ProposalBasicDetails = new ProposalDetailsProp();
            ProposalBasicDetails.PropertyChanged += ProposalBasicDetails_PropertyChanged;
            PageLoaderEnable = true; 
            ProposalBasicDetails.SurveyDate = DateTime.Now;
            ProposalBasicDetails.DateOfBirth = DateTime.Now;
            ProposalBasicDetails.SurveyTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            ProposalBasicDetails.PageCommandParamiter = "CreateProposalDetails";
            ProposalBasicDetails.CattleToBeInsuredEnable = true;

            //Edit
            EditProposalBasicDetails = new ProposalDetailsProp();
            //EditProposalBasicDetails.PropertyChanged += EditProposalBasicDetails_PropertyChanged;
            //EditProposalBasicDetails.SurveyDate = DateTime.Now;
            EditProposalBasicDetails.DateOfBirth = DateTime.Now;
            //EditProposalBasicDetails.SurveyTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            EditProposalBasicDetails.PageCommandParamiter = "EditProposalDetails";
            EditProposalBasicDetails.CattleToBeInsuredEnable = false;
            BindStateList();
            BindAllSchemeList();
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("editbasicdetailsKeys"))
            {
                var serializedModel = query["editbasicdetailsKeys"] as string;
                if (!string.IsNullOrEmpty(serializedModel))
                {
                    var SelectedPropId = serializedModel;
                    await BindStateList();
                    await BindAllSchemeList();
                    await EditBasicDetailsMethod(SelectedPropId);
                }
            }
           
        }
        
        private void ProposalBasicDetails_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ProposalBasicDetails.SelectedState))
            {
                District(ProposalBasicDetails.SelectedState.StateCode);
            }
            if (e.PropertyName == nameof(ProposalBasicDetails.SelectedDistrict))
            {
                BlockMethod(ProposalBasicDetails.SelectedDistrict.DistrictName);
            }
            if (e.PropertyName == nameof(ProposalBasicDetails.SelectedScheme))
            {
                GetAllSchemeMethod(ProposalBasicDetails.SelectedScheme.SchemeID);
            } 
           
        }

        public async Task EditBasicDetailsMethod(string SelectedPropId)
        {
            var item = await _editProposalService.GetEditProposalList(SelectedPropId);

            if (item.proposerDetailModel != null)
            {
                EditProposalId = item.proposerDetailModel.ProposalId;
                EditLeadNumber = item.proposerDetailModel.LeadNumber;
                EditProposalBasicDetails.SurveyDate = DateTime.Parse(item.proposerDetailModel.SurveyDate.Replace("?", "").Trim());
                EditProposalBasicDetails.CustomerName = item.proposerDetailModel.CustomerName;
                if (item.proposerDetailModel.CustomerGender != null)
                {
                    EditProposalBasicDetails.Gender = item.proposerDetailModel.CustomerGender;
                }
                EditProposalBasicDetails.SurveyTime= TimeSpan.Parse(item.proposerDetailModel.SurveyTime);

                EditProposalBasicDetails.FatherName = item.proposerDetailModel.FatherHusbandName;
                EditProposalBasicDetails.DateOfBirth = DateTime.Parse(item.proposerDetailModel.CustomerDOB.Replace("?", "").Trim());
                if (EditProposalBasicDetails.DateOfBirth != null)
                {
                    var calulateYear = CalculateAge(EditProposalBasicDetails.DateOfBirth);
                    EditProposalBasicDetails.Age = Convert.ToString(calulateYear) + " Years";
                }
                EditProposalBasicDetails.MobileNumber = item.proposerDetailModel.CustomerMobile;
                EditProposalBasicDetails.Address = item.proposerDetailModel.CustomerAddress;
                EditProposalBasicDetails.AdharNo = item.proposerDetailModel.AadharNumber;
              
                //Selected State_Distric_Block
                // var StateName = Regex.Replace(item.proposerDetailModel.State, @"\s*\(.*?\)", "");
                var StateName =item.proposerDetailModel.State;
                var statedata = ProposalBasicDetails.GetStateList
                        .Where(x => x.StateName == StateName)
                        .ToList();
                if (statedata.Any()) 
                {
                    EditProposalBasicDetails.SelectedState = statedata.First();
                    var statecode = EditProposalBasicDetails.SelectedState.StateCode;
                    //District Method
                    await District(statecode);
                    var DistricName = item.proposerDetailModel.District;
                    var Districtdata = EditProposalBasicDetails.GetDistrictList
                            .Where(x => x.DistrictName == DistricName)
                            .ToList();
                    if (Districtdata.Any())
                    {
                        EditProposalBasicDetails.SelectedDistrict = Districtdata.First();
                        var DistrictName = EditProposalBasicDetails.SelectedDistrict.DistrictName;

                        //Block Method
                        await BlockMethod(DistrictName);
                        var BlockName = item.proposerDetailModel.Block.Trim();
                        var BlockData = EditProposalBasicDetails.GetBlockList
                                .Where(x => x.TehsilName == BlockName)
                                .ToList();
                        if (BlockData.Any())
                        {
                            EditProposalBasicDetails.SelectedTehsil = BlockData.First();
                        }
                    }
                }
                EditProposalBasicDetails.FamilyStatus = item.proposerDetailModel.InpectionType;
                EditProposalBasicDetails.Category = item.proposerDetailModel.City;
                EditProposalBasicDetails.PinCode = item.proposerDetailModel.Pincode;
                EditProposalBasicDetails.NameOfBank = item.proposerDetailModel.NameOfBank;
                EditProposalBasicDetails.LoanAccountNo = item.proposerDetailModel.LoanAccNo;
                EditProposalBasicDetails.LoanAmount = item.proposerDetailModel.LoanAmount;
                EditProposalBasicDetails.PaymentReceived = item.proposerDetailModel.CauseOfDeath;
                EditProposalBasicDetails.PaymentMode = item.proposerDetailModel.Tagging;
                var schemData = Regex.Replace(item.proposerDetailModel.SchemeID, @"^\d+\s*", "");
                var schemSelect = EditProposalBasicDetails.GetAllSchemeList
                       .Where(x => x.SchemeName == schemData)
                       .ToList();
                if (schemSelect.Any())
                {
                    EditProposalBasicDetails.SelectedScheme = schemSelect.First();
                }
                //NoOfCattlesFunded
                EditProposalBasicDetails.NoOfCattleFunded = item.proposerDetailModel.NoOfCattlesFunde;
                EditProposalBasicDetails.CattleToBeInsured = item.proposerDetailModel.NumberOfCattleToBeInsured;
                //PolicyPeriod
                EditProposalBasicDetails.PolicyPeriod = item.proposerDetailModel.PolicyPeriod;
                
            }

        }

        private async Task BindStateList()
        {
            try
            {
                ProposalBasicDetails.GetStateList.Clear();
               
                var stateitem =await _baseDataService.GetState();

                if (stateitem != null)
                {
                    foreach (var item in stateitem)
                    {
                        BoState boState = new BoState()
                        {
                            StateCode=item.StateCode,
                            StateName=item.StateName.Trim()+" ("+ item.StateCode.Trim()+")"
                        };
                        ProposalBasicDetails.GetStateList.Add(boState);
                        EditProposalBasicDetails.GetStateList.Add(boState);
                    }
                }   
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("State List", ex.Message.ToString(), "OK");
            }
            
        }

        private async Task BindAllSchemeList()
        {
            try
            {
                ProposalBasicDetails.GetAllSchemeList.Clear();
                //GetAllSchemaList
                var SchemeData = await _baseDataService.GetAllSchemeData();
                if (SchemeData != null)
                {
                    foreach (var tag in SchemeData)
                    {
                        GetAllSchemeModel schemeData = new GetAllSchemeModel()
                        {
                            SchemeID = tag.SchemeID,
                            SchemeName = tag.SchemeName
                        };
                        ProposalBasicDetails.GetAllSchemeList.Add(schemeData);
                        EditProposalBasicDetails.GetAllSchemeList.Add(schemeData);
                    }
                }
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("All Scheme List", ex.Message.ToString(), "OK");
            }
        }
        public async Task District(string StateCode)
        {
            ProposalBasicDetails.GetDistrictList.Clear();
            if (StateCode != "")
            {
                try
                {

                    var data = await _baseDataService.GetDistric(StateCode);

                    foreach (var tag in data)
                    {
                        BoDistrict boDistrict = new BoDistrict()
                        {
                            DistrictId =  tag.DistrictId,
                            DistrictName = tag.DistrictName,
                        };
                        ProposalBasicDetails.GetDistrictList.Add(boDistrict);
                        EditProposalBasicDetails.GetDistrictList.Add(boDistrict);
                    }
                   
                }
                catch (Exception ex)
                {

                }
            }

           
        }

        public async Task BlockMethod(string DistrictName)
        {
            ProposalBasicDetails.GetBlockList.Clear();
            if (DistrictName != "")
            {
                try
                {
                    var BlockData = await _baseDataService.GetBlock(DistrictName);

                    foreach (var tag1 in BlockData)
                    {
                        BoTehsil boTehsil = new BoTehsil()
                        {
                            TehsilId=tag1.TehsilId,
                            TehsilName=tag1.TehsilName.Trim()

                        };
                        ProposalBasicDetails.GetBlockList.Add(boTehsil);
                        EditProposalBasicDetails.GetBlockList.Add(boTehsil);
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
        
        public async void GetAllSchemeMethod(int SchemeId)
        {
            try
            {
                var stateCode1 = "OR";
                var schemeIds = Convert.ToString(SchemeId);
                StateWiseSchemaList(stateCode1, schemeIds);
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        
        public async void StateWiseSchemaList(string stateCode, string schemeId)
        {
            if (stateCode != "")
            {
                try
                {
                    var schemeResult = await _baseDataService.GetStateWiseSchemaDataList(stateCode, schemeId);
                    if(schemeResult !=null)
                    {
                        foreach (var scheme in schemeResult)
                        {
                            Preferences.Set("FarmerPercentageValue1", scheme.BeneficiaryContribution);
                            Preferences.Set("GovPercentageValue1", scheme.StateShare);
                            Preferences.Set("GovCentralValue1", scheme.CentralShare);
                            Preferences.Set("TotalGovtShareValue1", scheme.TotalGovtShare);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

        }

        [RelayCommand]
        public async Task BasicDetails(object obj)
        {
            var Paramiter = obj.ToString();
            try
            {
                if (Paramiter == "CreateProposalDetails")
                {

                    ProposalBasicDetailModel basicDetailModel = new ProposalBasicDetailModel();
                    if (ProposalBasicDetails.SurveyDate.ToString() != null)
                    {
                        basicDetailModel.SurveyDate = ProposalBasicDetails.SurveyDate.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Survey Date", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.SurveyTime != TimeSpan.Zero)
                    {
                        basicDetailModel.SurveyTime = ProposalBasicDetails.SurveyTime.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Survey Time", "OK");
                        return;
                    }

                    if (ProposalBasicDetails.CustomerName != null)
                    {
                        if (Regex.Match(ProposalBasicDetails.CustomerName, "^[a-zA-Z ]*$").Success)
                        {
                            basicDetailModel.CustomerName = ProposalBasicDetails.CustomerName;
                            var custData = ProposalBasicDetails.MobileNumber + ProposalBasicDetails.CustomerName;
                            Preferences.Set("CustomerNameAndMobile", custData);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Customer Name", "ok");
                            return;

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Name", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.FatherName != null)
                    {
                        if (Regex.Match(ProposalBasicDetails.FatherName, "^[a-zA-Z ]*$").Success)
                        {
                            basicDetailModel.FatherHusbandName = ProposalBasicDetails.FatherName;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Father name/ Husband name", "ok");
                            return;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Father name/ Husband name", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.DateOfBirth.ToString() != null)
                    {

                        basicDetailModel.CustomerDOB = ProposalBasicDetails.DateOfBirth.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer DOB Date", "ok");
                        return;
                    }
                    //Gender
                    if (ProposalBasicDetails.Gender != null)
                    {

                        basicDetailModel.CustomerGender = ProposalBasicDetails.Gender.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Customer Gender", "ok");
                        return;
                    }
                    //Age Calculate
                    basicDetailModel.CustomerAge = ProposalBasicDetails.Age;

                    if (ProposalBasicDetails.MobileNumber != null)
                    {
                        if (Regex.Match(ProposalBasicDetails.MobileNumber, @"^\d{10}$").Success)
                        {
                            basicDetailModel.CustomerMobile = ProposalBasicDetails.MobileNumber;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Customer Mobile Number", "ok");
                            return;

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Mobile Number", "ok");
                        return;
                    }

                    basicDetailModel.PostOffice = "";
                    if (ProposalBasicDetails.Address != null)
                    {
                        basicDetailModel.CustomerAddress = ProposalBasicDetails.Address.Trim();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Address", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.AdharNo != null)
                    {
                        if (Regex.Match(ProposalBasicDetails.AdharNo, @"^\d{12}$").Success)
                        {
                            basicDetailModel.AadharNumber = ProposalBasicDetails.AdharNo;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Aadhar Number", "ok");
                            return;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Aadhar Number", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.SelectedState.ToString() != "-1")
                    {
                        basicDetailModel.State = ProposalBasicDetails.SelectedState.StateName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select state", "ok");
                        return;
                    }


                    if (ProposalBasicDetails.SelectedDistrict.DistrictName.ToString() != "-1")
                    {
                        basicDetailModel.District = ProposalBasicDetails.SelectedDistrict.DistrictName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select District", "ok");
                        return;
                    }
                    if (ProposalBasicDetails.SelectedTehsil != null)
                    {
                        basicDetailModel.Block = ProposalBasicDetails.SelectedTehsil.TehsilName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select Block", "ok");
                        return;
                    }

                    basicDetailModel.GramPanchayat = "";

                    if (ProposalBasicDetails.Category.ToString() != "-1")
                    {
                        basicDetailModel.City = ProposalBasicDetails.Category.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Customer Catagory", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.FamilyStatus.ToString() != "-1")
                    {
                        basicDetailModel.InpectionType = ProposalBasicDetails.FamilyStatus.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Family Status", "ok");
                        return;
                    }


                    if (ProposalBasicDetails.PinCode != null)
                    {

                        if (Regex.Match(ProposalBasicDetails.PinCode, @"^\d{6}$").Success)
                        {
                            basicDetailModel.Pincode = ProposalBasicDetails.PinCode;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Pin code", "ok");
                            return;

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Pin code", "ok");
                        return;
                    }

                    if (ProposalBasicDetails.NameOfBank != null)
                    {
                        basicDetailModel.NameOfBank = ProposalBasicDetails.NameOfBank;
                    }
                    else
                    {
                        basicDetailModel.NameOfBank = "";
                        //await App.Current.MainPage.DisplayAlert("Alert", "Enter Name Of Bank", "ok");
                        //return;
                    }
                    if (ProposalBasicDetails.LoanAccountNo != null)
                    {
                        basicDetailModel.LoanAccNo = ProposalBasicDetails.LoanAccountNo;
                    }
                    else
                    {
                        basicDetailModel.LoanAccNo = "";
                        //await App.Current.MainPage.DisplayAlert("Alert", "Enter Loan Acc No", "ok");
                        //return;
                    }
                    if (ProposalBasicDetails.LoanAmount != null)
                    {
                        basicDetailModel.LoanAmount = ProposalBasicDetails.LoanAmount;
                    }
                    else
                    {
                        basicDetailModel.LoanAmount = "";
                        //await App.Current.MainPage.DisplayAlert("Alert", "Enter Loan Amount", "ok");
                        //return;
                    }

                    if (Convert.ToString(ProposalBasicDetails.PaymentReceived) != null)
                    {
                        basicDetailModel.CauseOfDeath = Convert.ToString(ProposalBasicDetails.PaymentReceived);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select Payment Received", "ok");
                        return;
                    }
                    if (Convert.ToString(ProposalBasicDetails.PaymentMode) != null)
                    {
                        basicDetailModel.Tagging = Convert.ToString(ProposalBasicDetails.PaymentMode);
                    }
                    else
                    {
                        basicDetailModel.Tagging = "";
                    }
                    //GetAllScheam
                    if (ProposalBasicDetails.SelectedScheme != null)
                    {
                        var mergData = ProposalBasicDetails.SelectedScheme.SchemeID.ToString() + " " + ProposalBasicDetails.SelectedScheme.SchemeName.ToString();
                        basicDetailModel.SchemeID = mergData;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select All Scheam", "ok");
                        return;
                    }

                    //NoOfCattlesFunded
                    if (Convert.ToString(ProposalBasicDetails.NoOfCattleFunded) != null)
                    {
                        var tempCattlesFunded = ProposalBasicDetails.NoOfCattleFunded;
                        basicDetailModel.NoOfCattlesFunde = tempCattlesFunded.ToString();

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select No Of Cattles Funded", "ok");
                        return;
                    }
                    //NumberOfCattleToBeInsured
                    if (Convert.ToString(ProposalBasicDetails.CattleToBeInsured) != null)
                    {
                        var CattleToBeInsured = Convert.ToInt32(ProposalBasicDetails.CattleToBeInsured);
                        if (Convert.ToInt32(ProposalBasicDetails.CattleToBeInsured) >= CattleToBeInsured)
                        {
                            basicDetailModel.NumberOfCattleToBeInsured = CattleToBeInsured.ToString();

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "CattleToBeInsured value Greater then NoOfCattlesFunded value", "ok");
                            return;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select CattleToBeInsured", "ok");
                        return;
                    }
                    //PolicyPeriod
                    if (ProposalBasicDetails.PolicyPeriod != null)
                    {
                        basicDetailModel.PolicyPeriod = ProposalBasicDetails.PolicyPeriod;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select Policy Period", "ok");
                        return;
                    }
                    var Userdt = Preferences.Get("UserId", "");
                    basicDetailModel.CreateBy = Userdt;
                    //API Call for Data Save
                    var SaveBasicData = await _createProposalService.SaveBasicDetails(basicDetailModel);
                    var test = SaveBasicData as CreateProposalResult;
                    if (test != null)
                    {
                        CreateProposalResult create = new CreateProposalResult()
                        {
                            PropId = test.PropId,
                            LeadNumber = test.LeadNumber,
                            SumInsured = test.ReturnCattleToBeInsured,
                            //SumInsured =test.SumInsured,
                            Premium = test.Premium,
                            QuoteId = test.QuoteId,
                            TotalPremium = test.TotalPremium,
                        };
                        // var dt = int.Parse(test.SumInsured);

                        if (create != null)
                        {
                            // test.SumInsured = dt.ToString();
                            var serializedModel = JsonConvert.SerializeObject(create);
                            var encodedModel = Uri.EscapeDataString(serializedModel);
                            await Shell.Current.GoToAsync($"//createProposal/animalDetails?basicdetailsKey={encodedModel}");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "SumInsured value is invalid.", "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "SaveBasicData is not of type CreateProposalResult.", "OK");
                    }
                    
                    var PropId = SaveBasicData.PropId;
                    var LeadId = SaveBasicData.LeadNumber;
                    Preferences.Set("CreateProposalSavePropId", PropId);
                    Preferences.Set("CreateProposalSaveLeadId", LeadId);
                }
             

                else if (Paramiter == "EditProposalDetails")
                {

                    ProposalBasicDetailModel basicDetailModel1 = new ProposalBasicDetailModel();
                    basicDetailModel1.Propid = EditProposalId;
                    basicDetailModel1.LeadNumber=EditLeadNumber;
                    if (EditProposalBasicDetails.SurveyDate.ToString() != null)
                    {
                        basicDetailModel1.SurveyDate = EditProposalBasicDetails.SurveyDate.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Survey Date", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.SurveyTime != TimeSpan.Zero)
                    {
                        basicDetailModel1.SurveyTime = EditProposalBasicDetails.SurveyTime.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Survey Time", "OK");
                        return;
                    }

                    if (EditProposalBasicDetails.CustomerName != null)
                    {
                        if (Regex.Match(EditProposalBasicDetails.CustomerName, "^[a-zA-Z ]*$").Success)
                        {
                            basicDetailModel1.CustomerName = EditProposalBasicDetails.CustomerName;
                            var custData = EditProposalBasicDetails.MobileNumber + EditProposalBasicDetails.CustomerName;
                            Preferences.Set("CustomerNameAndMobile", custData);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Customer Name", "ok");
                            return;

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Name", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.FatherName != null)
                    {
                        if (Regex.Match(EditProposalBasicDetails.FatherName, "^[a-zA-Z ]*$").Success)
                        {
                            basicDetailModel1.FatherHusbandName = EditProposalBasicDetails.FatherName;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Father name/ Husband name", "ok");
                            return;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Father name/ Husband name", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.DateOfBirth.ToString() != null)
                    {

                        basicDetailModel1.CustomerDOB = EditProposalBasicDetails.DateOfBirth.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer DOB Date", "ok");
                        return;
                    }
                    //Gender
                    if (EditProposalBasicDetails.Gender != null)
                    {

                        basicDetailModel1.CustomerGender = EditProposalBasicDetails.Gender.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Customer Gender", "ok");
                        return;
                    }
                    //Age Calculate
                    basicDetailModel1.CustomerAge = EditProposalBasicDetails.Age;

                    if (EditProposalBasicDetails.MobileNumber != null)
                    {
                        if (Regex.Match(EditProposalBasicDetails.MobileNumber, @"^\d{10}$").Success)
                        {
                            basicDetailModel1.CustomerMobile = EditProposalBasicDetails.MobileNumber;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Enter Customer Mobile Number", "ok");
                            return;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Mobile Number", "ok");
                        return;
                    }

                    basicDetailModel1.PostOffice = "";
                    if (EditProposalBasicDetails.Address != null)
                    {
                        basicDetailModel1.CustomerAddress = EditProposalBasicDetails.Address.Trim();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Enter Customer Address", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.AdharNo != null)
                    {
                        if (Regex.Match(EditProposalBasicDetails.AdharNo, @"^\d{12}$").Success)
                        {
                            basicDetailModel1.AadharNumber = EditProposalBasicDetails.AdharNo;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Aadhar Number", "ok");
                            return;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Aadhar Number", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.SelectedState.ToString() != "-1")
                    {
                        basicDetailModel1.State = EditProposalBasicDetails.SelectedState.StateName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select state", "ok");
                        return;
                    }


                    if (EditProposalBasicDetails.SelectedDistrict.DistrictName.ToString() != "-1")
                    {
                        basicDetailModel1.District = EditProposalBasicDetails.SelectedDistrict.DistrictName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select District", "ok");
                        return;
                    }
                    if (EditProposalBasicDetails.SelectedTehsil != null)
                    {
                        basicDetailModel1.Block = EditProposalBasicDetails.SelectedTehsil.TehsilName.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "select Block", "ok");
                        return;
                    }

                    basicDetailModel1.GramPanchayat = "";

                    if (EditProposalBasicDetails.Category.ToString() != "-1")
                    {
                        basicDetailModel1.City = EditProposalBasicDetails.Category.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Customer Catagory", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.FamilyStatus.ToString() != "-1")
                    {
                        basicDetailModel1.InpectionType = EditProposalBasicDetails.FamilyStatus.ToString();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Select Family Status", "ok");
                        return;
                    }


                    if (EditProposalBasicDetails.PinCode != null)
                    {

                        if (Regex.Match(EditProposalBasicDetails.PinCode, @"^\d{6}$").Success)
                        {
                            basicDetailModel1.Pincode = EditProposalBasicDetails.PinCode;
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Pin code", "ok");
                            return;

                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "Please Enter Valid Pin code", "ok");
                        return;
                    }

                    if (EditProposalBasicDetails.NameOfBank != null)
                    {
                        basicDetailModel1.NameOfBank = EditProposalBasicDetails.NameOfBank;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Name Of Bank", "ok");
                        return;
                    }
                    if (EditProposalBasicDetails.LoanAccountNo != null)
                    {
                        basicDetailModel1.LoanAccNo = EditProposalBasicDetails.LoanAccountNo;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Loan Acc No", "ok");
                        return;
                    }
                    if (EditProposalBasicDetails.LoanAmount != null)
                    {
                        basicDetailModel1.LoanAmount = EditProposalBasicDetails.LoanAmount;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Enter Loan Amount", "ok");
                        return;
                    }

                    if (Convert.ToString(EditProposalBasicDetails.PaymentReceived) != null)
                    {
                        basicDetailModel1.CauseOfDeath = Convert.ToString(EditProposalBasicDetails.PaymentReceived);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select Payment Received", "ok");
                        return;
                    }
                    if (Convert.ToString(EditProposalBasicDetails.PaymentMode) != null)
                    {
                        basicDetailModel1.Tagging = Convert.ToString(EditProposalBasicDetails.PaymentMode);
                    }
                    else
                    {
                        basicDetailModel1.Tagging = "";
                    }
                    //GetAllScheam
                    if (EditProposalBasicDetails.SelectedScheme != null)
                    {
                        var mergData = EditProposalBasicDetails.SelectedScheme.SchemeID.ToString() + " " + EditProposalBasicDetails.SelectedScheme.SchemeName.ToString();
                        basicDetailModel1.SchemeID = mergData;

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select All Scheam", "ok");
                        return;
                    }

                    //NoOfCattlesFunded
                    if (Convert.ToString(EditProposalBasicDetails.NoOfCattleFunded) != null)
                    {
                        var tempCattlesFunded = EditProposalBasicDetails.NoOfCattleFunded;
                        basicDetailModel1.NoOfCattlesFunde = tempCattlesFunded.ToString();

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select No Of Cattles Funded", "ok");
                        return;
                    }
                    //NumberOfCattleToBeInsured
                    if (Convert.ToString(EditProposalBasicDetails.CattleToBeInsured) != null)
                    {
                        var CattleToBeInsured = Convert.ToInt32(EditProposalBasicDetails.CattleToBeInsured);
                        if (Convert.ToInt32(EditProposalBasicDetails.CattleToBeInsured) >= CattleToBeInsured)
                        {
                            basicDetailModel1.NumberOfCattleToBeInsured = CattleToBeInsured.ToString();

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "CattleToBeInsured value Greater then NoOfCattlesFunded value", "ok");
                            return;
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select CattleToBeInsured", "ok");
                        return;
                    }
                    //PolicyPeriod
                    if (EditProposalBasicDetails.PolicyPeriod != null)
                    {
                        basicDetailModel1.PolicyPeriod = EditProposalBasicDetails.PolicyPeriod;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Select Policy Period", "ok");
                        return;
                    }
                    var Userdt = Preferences.Get("UserId", "");
                    basicDetailModel1.CreateBy = Userdt;

                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(basicDetailModel1); 
                    //API Call for Data Save
                    var SaveEditBasicData = await _editProposalService.SaveEditProposalBasicDetails(basicDetailModel1);
                   
                    var test = SaveEditBasicData as CreateProposalResult;
                    if (test != null)
                    {
                        EditProposalPageLoaderEnable = true;
                        CreateProposalResult editCreate = new CreateProposalResult()
                        {
                            PropId = test.PropId,
                            LeadNumber = test.LeadNumber,
                            SumInsured = test.ReturnCattleToBeInsured,
                            Premium = test.Premium,
                            QuoteId = test.QuoteId,
                            TotalPremium = test.TotalPremium,
                        };
                        var serializedModel = JsonConvert.SerializeObject(editCreate);
                        var encodedModel = Uri.EscapeDataString(serializedModel);
                      
                        await Shell.Current.GoToAsync($"///editPreviewPage/editAnimalCardPage?EditAnimaldetailsKeys={encodedModel}");
                        Preferences.Set("EditProposalSavePropId", editCreate.PropId);
                        Preferences.Set("EditProposalSaveLeadId", editCreate.LeadNumber);
                        EditProposalPageLoaderEnable = false;
                    }

                }

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }

        public int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - birthDate.Year;
            // Check if the birth date has already occurred this year
            if (birthDate > currentDate.AddYears(-age))
                age--;
            EditProposalBasicDetails.Age = age + " " + "Years";
            return age;
        }
    }
}
