using CattelSalasarMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.HelperModels
{
    public partial class ClaimAnimalImageCardClass : BaseViewModel, INotifyPropertyChanged
    {
       
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
        public bool _localSaveButtomVisible;
        public bool LocalSaveButtomVisible
        {
            get => _localSaveButtomVisible;
            set
            {
                if (_localSaveButtomVisible != value)
                {
                    _localSaveButtomVisible = value;
                    OnPropertyChanged(nameof(LocalSaveButtomVisible));
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
                if (_seventhImage != value)
                {
                    _seventhImage = value;
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
         public ImageSource _eleventhImage;
        public ImageSource EleventhImage
        {
            get => _eleventhImage;
            set
            {
                if (_eleventhImage != value)
                {
                    _eleventhImage = value;
                    OnPropertyChanged(nameof(EleventhImage));
                }
            }
        }
         public ImageSource _twelthImage;
        public ImageSource TwelthImage
        {
            get => _twelthImage;
            set
            {
                if (_twelthImage != value)
                {
                    _twelthImage = value;
                    OnPropertyChanged(nameof(TwelthImage));
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

