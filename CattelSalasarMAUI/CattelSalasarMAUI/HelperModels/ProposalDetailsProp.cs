using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CattelSalasarMAUI.Models;

namespace CattelSalasarMAUI.HelperModels
{
    public class ProposalDetailsProp : INotifyPropertyChanged
    {
        private DateTime _surveyDate;
        public DateTime SurveyDate
        {
            get => _surveyDate;
            set
            {
                if (_surveyDate != value)
                {
                    _surveyDate = value;
                    OnPropertyChanged(nameof(SurveyDate));
                }
            }
        }

        public TimeSpan _surveyTime;
        public TimeSpan SurveyTime
        {
            get => _surveyTime;
            set
            {
                if (_surveyTime != value)
                {
                    _surveyTime = value;
                    OnPropertyChanged(nameof(SurveyTime));
                }
            }
        }
      
        private string _customerName;
        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        private string _fatherName;
        public string FatherName
        {
            get => _fatherName;
            set
            {
                if (_fatherName != value)
                {
                    _fatherName = value;
                    OnPropertyChanged(nameof(FatherName));
                }
            }
        }

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        private string _familyStatus;
        public string FamilyStatus
        {
            get => _familyStatus;
            set
            {
                if (_familyStatus != value)
                {
                    _familyStatus = value;
                    OnPropertyChanged(nameof(FamilyStatus));
                }
            }
        }
        // public string DateOfBirth { get; set; }
        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;
            set
            {
                if (_mobileNumber != value)
                {
                    _mobileNumber = value;
                    OnPropertyChanged(nameof(MobileNumber));
                }
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private string _adharNo;
        public string AdharNo
        {
            get => _adharNo;
            set
            {
                if (_adharNo != value)
                {
                    _adharNo = value;
                    OnPropertyChanged(nameof(AdharNo));
                }
            }
        }
        //public BoState SelectedState { get; set; }
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
                }
            }
        }

        private BoTehsil _selectedTehsil;
        public BoTehsil SelectedTehsil
        {
            get => _selectedTehsil;
            set
            {
                if (_selectedTehsil != value)
                {
                    _selectedTehsil = value;
                    OnPropertyChanged(nameof(SelectedTehsil));
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

        private string _schemeName;
        public string SchemeName
        {
            get => _schemeName;
            set
            {
                if (_schemeName != value)
                {
                    _schemeName = value;
                    OnPropertyChanged(nameof(SchemeName));
                }
            }
        }

        private string _tehsilName;
        public string TehsilName
        {
            get => _tehsilName;
            set
            {
                if (_tehsilName != value)
                {
                    _tehsilName = value;
                    OnPropertyChanged(nameof(TehsilName));
                }
            }
        }

        private string _pinCode;
        public string PinCode
        {
            get => _pinCode;
            set
            {
                if (_pinCode != value)
                {
                    _pinCode = value;
                    OnPropertyChanged(nameof(PinCode));
                }
            }
        }

        private string _nameOfBank;
        public string NameOfBank
        {
            get => _nameOfBank;
            set
            {
                if (_nameOfBank != value)
                {
                    _nameOfBank = value;
                    OnPropertyChanged(nameof(NameOfBank));
                }
            }
        }

        private string _loanAccountNo;
        public string LoanAccountNo
        {
            get => _loanAccountNo;
            set
            {
                if (_loanAccountNo != value)
                {
                    _loanAccountNo = value;
                    OnPropertyChanged(nameof(LoanAccountNo));
                }
            }
        }

        private string _loanAmount;
        public string LoanAmount
        {
            get => _loanAmount;
            set
            {
                if (_loanAmount != value)
                {
                    _loanAmount = value;
                    OnPropertyChanged(nameof(LoanAmount));
                }
            }
        }

        private string _noOfCattleFunded;
        public string NoOfCattleFunded
        {
            get => _noOfCattleFunded;
            set
            {
                if (_noOfCattleFunded != value)
                {
                    _noOfCattleFunded = value;
                    OnPropertyChanged(nameof(NoOfCattleFunded));
                }
            }
        }

        private string _cattleToBeInsured;
        public string CattleToBeInsured
        {
            get => _cattleToBeInsured;
            set
            {
                if (_cattleToBeInsured != value)
                {
                    _cattleToBeInsured = value;
                    OnPropertyChanged(nameof(CattleToBeInsured));
                }
            }
        }

        private bool _cattleToBeInsuredEnable;
        public bool CattleToBeInsuredEnable
        {
            get => _cattleToBeInsuredEnable;
            set
            {
                if (_cattleToBeInsuredEnable != value)
                {
                    _cattleToBeInsuredEnable = value;
                    OnPropertyChanged(nameof(CattleToBeInsuredEnable));
                }
            }
        }


        private string _paymentReceived;
        public string PaymentReceived
        {
            get => _paymentReceived;
            set
            {
                if (_paymentReceived != value)
                {
                    _paymentReceived = value;
                    OnPropertyChanged(nameof(PaymentReceived));
                }
            }
        }
        private string _paymentMode;
        public string PaymentMode
        {
            get => _paymentMode;
            set
            {
                if (_paymentMode != value)
                {
                    _paymentMode = value;
                    OnPropertyChanged(nameof(PaymentMode));
                }
            }
        }

        private string _policyPeriod;
        public string PolicyPeriod
        {
            get => _policyPeriod;
            set
            {
                if (_policyPeriod != value)
                {
                    _policyPeriod = value;
                    OnPropertyChanged(nameof(PolicyPeriod));
                }
            }
        }

        private string _getAllSchemeData;
        public string GetAllSchemeData
        {
            get => _getAllSchemeData;
            set
            {
                if (_getAllSchemeData != value)
                {
                    _getAllSchemeData = value;
                    OnPropertyChanged(nameof(GetAllSchemeData));
                }
            }
        }
        
        private string _pageCommandParamiter;
        public string PageCommandParamiter
        {
            get => _pageCommandParamiter;
            set
            {
                if (_pageCommandParamiter != value)
                {
                    _pageCommandParamiter = value;
                    OnPropertyChanged(nameof(PageCommandParamiter));
                }
            }
        }

        public ObservableCollection<BoState> GetStateList { get; set; }
        public ObservableCollection<BoDistrict> GetDistrictList { get; set; }
        public ObservableCollection<BoTehsil> GetBlockList { get; set; }
        public ObservableCollection<GetAllSchemeModel> GetAllSchemeList { get; set; }
        public ProposalDetailsProp()
        {
            GetStateList = new ObservableCollection<BoState>();
            GetDistrictList = new ObservableCollection<BoDistrict>();
            GetBlockList = new ObservableCollection<BoTehsil>();
            GetAllSchemeList = new ObservableCollection<GetAllSchemeModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }

}
