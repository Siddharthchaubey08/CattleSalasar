using CattelSalasarMAUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.HelperModels
{
    public partial class ProposalAnimalDetailClass : BaseViewModel, INotifyPropertyChanged
    {
        private string _animalDataID;
        public string AnimalDataID
        {
            get => _animalDataID;
            set
            {
                if (_animalDataID != value)
                {
                    _animalDataID = value;
                    OnPropertyChanged(nameof(AnimalDataID));
                }
            }
        }
         private string _propId;
        public string PropId
        {
            get => _propId;
            set
            {
                if (_propId != value)
                {
                    _propId = value;
                    OnPropertyChanged(nameof(PropId));
                }
            }
        }

        private string _selectedAnimalType;
        public string SelectedAnimalType
        {
            get => _selectedAnimalType;
            set
            {
                if (_selectedAnimalType != value)
                {
                    _selectedAnimalType = value;
                    OnPropertyChanged(nameof(SelectedAnimalType));
                }
            }
        }

        private string _selectedCategoryType;
        public string SelectedCategoryType
        {
            get => _selectedCategoryType;
            set
            {
                if (_selectedCategoryType != value)
                {
                    _selectedCategoryType = value;
                    OnPropertyChanged(nameof(SelectedCategoryType));
                }
            }
        }

        private string _selectedSwitchOfTail;
        public string SelectedSwitchOfTail
        {
            get => _selectedSwitchOfTail;
            set
            {
                if (_selectedSwitchOfTail != value)
                {
                    _selectedSwitchOfTail = value;
                    OnPropertyChanged(nameof(SelectedSwitchOfTail));
                }
            }
        }

        private string _breedName;
        public string BreedName
        {
            get => _breedName;
            set
            {
                if (_breedName != value)
                {
                    _breedName = value;
                    OnPropertyChanged(nameof(BreedName));
                }
            }
        }

        private string _bodyColour;
        public string BodyColour
        {
            get => _bodyColour;
            set
            {
                if (_bodyColour != value)
                {
                    _bodyColour = value;
                    OnPropertyChanged(nameof(BodyColour));
                }
            }
        }
       
        private Color _cameraToggelBg;
        public Color CameraToggelBg
        {
            get => _cameraToggelBg;
            set
            {
                if (_cameraToggelBg != value)
                {
                    _cameraToggelBg = value;
                    OnPropertyChanged(nameof(CameraToggelBg));
                }
            }
        }

        private Color _galleryToggelBg;
        public Color GalleryToggelBg
        {
            get => _galleryToggelBg;
            set
            {
                if (_galleryToggelBg != value)
                {
                    _galleryToggelBg = value;
                    OnPropertyChanged(nameof(GalleryToggelBg));
                }
            }
        }


        private string _selectedSex;
        public string SelectedSex
        {
            get => _selectedSex;
            set
            {
                if (_selectedSex != value)
                {
                    _selectedSex = value;
                    OnPropertyChanged(nameof(SelectedSex));
                }
            }
        }

        private string _approxAgeOfAnimal;
        public string ApproxAgeOfAnimal
        {
            get => _approxAgeOfAnimal;
            set
            {
                if (_approxAgeOfAnimal != value)
                {
                    _approxAgeOfAnimal = value;
                    OnPropertyChanged(nameof(ApproxAgeOfAnimal));
                }
            }
        }

        private string _milkingStatus;
        public string MilkingStatus
        {
            get => _milkingStatus;
            set
            {
                if (_milkingStatus != value)
                {
                    _milkingStatus = value;
                    OnPropertyChanged(nameof(MilkingStatus));
                }
            }
        }

        private string _milkYield;
        public string MilkYield
        {
            get => _milkYield;
            set
            {
                if (_milkYield != value)
                {
                    _milkYield = value;
                    OnPropertyChanged(nameof(MilkYield));
                }
            }
        }

        private string _selectedPregnancyStatus;
        public string SelectedPregnancyStatus
        {
            get => _selectedPregnancyStatus;
            set
            {
                if (_selectedPregnancyStatus != value)
                {
                    _selectedPregnancyStatus = value;
                    OnPropertyChanged(nameof(SelectedPregnancyStatus));
                }
            }
        }

        private string _selectedVaccination;
        public string SelectedVaccination
        {
            get => _selectedVaccination;
            set
            {
                if (_selectedVaccination != value)
                {
                    _selectedVaccination = value;
                    OnPropertyChanged(nameof(SelectedVaccination));
                }
            }
        }

        private string _otherIdentificationMark;
        public string OtherIdentificationMark
        {
            get => _otherIdentificationMark;
            set
            {
                if (_otherIdentificationMark != value)
                {
                    _otherIdentificationMark = value;
                    OnPropertyChanged(nameof(OtherIdentificationMark));
                }
            }
        }

        private string _drContactName;
        public string DrContactName
        {
            get => _drContactName;
            set
            {
                if (_drContactName != value)
                {
                    _drContactName = value;
                    OnPropertyChanged(nameof(DrContactName));
                }
            }
        }

        private string _drContactNo;
        public string DrContactNo
        {
            get => _drContactNo;
            set
            {
                if (_drContactNo != value)
                {
                    _drContactNo = value;
                    OnPropertyChanged(nameof(DrContactNo));
                }
            }
        }
        
        private string _registrationNumber;
        public string RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                if (_registrationNumber != value)
                {
                    _registrationNumber = value;
                    OnPropertyChanged(nameof(RegistrationNumber));
                }
            }
        }
       
        private string _healthCertificateIssueDate;
        public string HealthCertificateIssueDate
        {
            get => _healthCertificateIssueDate;
            set
            {
                if (_healthCertificateIssueDate != value)
                {
                    _healthCertificateIssueDate = value;
                    OnPropertyChanged(nameof(HealthCertificateIssueDate));
                }
            }
        }
        
        private string _selectedTypeOfTagging;
        public string SelectedTypeOfTagging
        {
            get => _selectedTypeOfTagging;
            set
            {
                if (_selectedTypeOfTagging != value)
                {
                    _selectedTypeOfTagging = value;
                    OnPropertyChanged(nameof(SelectedTypeOfTagging));
                }
            }
        }
        
        private string _selectedOwnershipDetails;
        public string SelectedOwnershipDetails
        {
            get => _selectedOwnershipDetails;
            set
            {
                if (_selectedOwnershipDetails != value)
                {
                    _selectedOwnershipDetails = value;
                    OnPropertyChanged(nameof(SelectedOwnershipDetails));
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
       
        private string _paymentUTRNumber;
        public string PaymentUTRNumber
        {
            get => _paymentUTRNumber;
            set
            {
                if (_paymentUTRNumber != value)
                {
                    _paymentUTRNumber = value;
                    OnPropertyChanged(nameof(PaymentUTRNumber));
                }
            }
        }
        
        private string _premiumRate;
        public string PremiumRate
        {
            get => _premiumRate;
            set
            {
                if (_premiumRate != value)
                {
                    _premiumRate = value;
                    OnPropertyChanged(nameof(PremiumRate));
                }
            }
        }
        
        private string _sumInsured;
        public string SumInsured
        {
            get => _sumInsured;
            set
            {
                if (_sumInsured != value)
                {
                    _sumInsured = value;
                    OnPropertyChanged(nameof(SumInsured));
                }
            }
        }

        private string _premiumAmount;
        public string PremiumAmount
        {
            get => _premiumAmount;
            set
            {
                if (_premiumAmount != value)
                {
                    _premiumAmount = value;
                    OnPropertyChanged(nameof(PremiumAmount));
                }
            }
        }

        private string _farmerPremiumPercentage;
        public string FarmerPremiumPercentage
        {
            get => _farmerPremiumPercentage;
            set
            {
                if (_farmerPremiumPercentage != value)
                {
                    _farmerPremiumPercentage = value;
                    OnPropertyChanged(nameof(FarmerPremiumPercentage));
                }
            }
        }
        
        private string _govPremiumPercentage;
        public string GovPremiumPercentage
        {
            get => _govPremiumPercentage;
            set
            {
                if (_govPremiumPercentage != value)
                {
                    _govPremiumPercentage = value;
                    OnPropertyChanged(nameof(GovPremiumPercentage));
                }
            }
        }
        
        private string _centralSharePercentage;
        public string CentralSharePercentage
        {
            get => _centralSharePercentage;
            set
            {
                if (_centralSharePercentage != value)
                {
                    _centralSharePercentage = value;
                    OnPropertyChanged(nameof(CentralSharePercentage));
                }
            }
        }
        
        private string _testComman;
        public string TestComman
        {
            get => _testComman;
            set
            {
                if (_testComman != value)
                {
                    _testComman = value;
                    OnPropertyChanged(nameof(TestComman));
                }
            }
        }

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
        public bool _imageSection;
        public bool ImageSection
        {
            get => _imageSection;
            set
            {
                if (_imageSection != value)
                {
                    _imageSection = value;
                    OnPropertyChanged(nameof(ImageSection));
                }
            }
        }
        public bool _animalDetailsSection;
        public bool AnimalDetailsSection
        {
            get => _animalDetailsSection;
            set
            {
                if (_animalDetailsSection != value)
                {
                    _animalDetailsSection = value;
                    OnPropertyChanged(nameof(AnimalDetailsSection));
                }
            }
        }

        public bool _imageSaveToServerVisible;
        public bool ImageSaveToServerVisible
        {
            get => _imageSaveToServerVisible;
            set
            {
                if (_imageSaveToServerVisible != value)
                {
                    _imageSaveToServerVisible = value;
                    OnPropertyChanged(nameof(ImageSaveToServerVisible));
                }
            }
        }

        public bool _imageSaveToDbVisible;
        public bool ImageSaveToDbVisible
        {
            get => _imageSaveToDbVisible;
            set
            {
                if (_imageSaveToDbVisible != value)
                {
                    _imageSaveToDbVisible = value;
                    OnPropertyChanged(nameof(ImageSaveToDbVisible));
                }
            }
        }


        
        public string _currentAnimalNumber;
        public string CurrentAnimalNumber
        {
            get => _currentAnimalNumber;
            set
            {
                if (_currentAnimalNumber != value)
                {
                    _currentAnimalNumber = value;
                    OnPropertyChanged(nameof(CurrentAnimalNumber));
                }
            }
        }
        public string _selectedPDFFileName;
        public string SelectedPDFFileName
        {
            get => _selectedPDFFileName;
            set
            {
                if (_selectedPDFFileName != value)
                {
                    _selectedPDFFileName = value;
                    OnPropertyChanged(nameof(SelectedPDFFileName));
                }
            }
        }

        //Image Section
        public ImageSource _firstImage;
        public ImageSource FirstImage
        {
            get => _firstImage;
            set
            {
                if (_firstImage != value)
                {
                    _firstImage = value;
                    OnPropertyChanged(nameof(FirstImage));
                }
            }
        }

        public ImageSource _secondImage;
        public ImageSource SecondImage
        {
            get => _secondImage;
            set
            {
                if (_secondImage != value)
                {
                    _secondImage = value;
                    OnPropertyChanged(nameof(SecondImage));
                }
            }
        }

        public ImageSource _thirdImage;
        public ImageSource ThirdImage
        {
            get => _thirdImage;
            set
            {
                if (_thirdImage != value)
                {
                    _thirdImage = value;
                    OnPropertyChanged(nameof(ThirdImage));
                }
            }
        }

        public ImageSource _fourthImage;
        public ImageSource FourthImage
        {
            get => _fourthImage;
            set
            {
                if (_fourthImage != value)
                {
                    _fourthImage = value;
                    OnPropertyChanged(nameof(FourthImage));
                }
            }
        }

        public ImageSource _fivethImage;
        public ImageSource FivethImage
        {
            get => _fivethImage;
            set
            {
                if (_fivethImage != value)
                {
                    _fivethImage = value;
                    OnPropertyChanged(nameof(FivethImage));
                }
            }
        }

        public ImageSource _sixthImage;
        public ImageSource SixthImage
        {
            get => _sixthImage;
            set
            {
                if (_sixthImage != value)
                {
                    _sixthImage = value;
                    OnPropertyChanged(nameof(SixthImage));
                }
            }
        }

        public ImageSource _seventhImage;
        public ImageSource SeventhImage
        {
            get => _seventhImage;
            set
            {
                if (_seventhImage != value)
                {
                    _seventhImage = value;
                    OnPropertyChanged(nameof(SeventhImage));
                }
            }
        }

        public ImageSource _eighthImage;
        public ImageSource EighthImage
        {
            get => _eighthImage;
            set
            {
                if (_eighthImage != value)
                {
                    _eighthImage = value;
                    OnPropertyChanged(nameof(EighthImage));
                }
            }
        }

        public ImageSource _ninthImage;
        public ImageSource NinthImage
        {
            get => _ninthImage;
            set
            {
                if (_ninthImage != value)
                {
                    _ninthImage = value;
                    OnPropertyChanged(nameof(NinthImage));
                }
            }
        }

        public ImageSource _tenthImage;
        public ImageSource TenthImage
        {
            get => _tenthImage;
            set
            {
                if (_tenthImage != value)
                {
                    _tenthImage = value;
                    OnPropertyChanged(nameof(TenthImage));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
