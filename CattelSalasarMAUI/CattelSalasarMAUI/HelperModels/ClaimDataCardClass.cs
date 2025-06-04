using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.HelperModels
{
    public partial class ClaimDataCardClass : BaseViewModel
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

        private string _customerMobile;
        public string CustomerMobile
        {
            get => _customerMobile;
            set
            {
                if (_customerMobile != value)
                {
                    _customerMobile = value;
                    OnPropertyChanged(nameof(CustomerMobile));
                }
            }
        }

        private IntimationCardModel _selectedClaimIntimation;
        public IntimationCardModel SelectedClaimIntimation

        {
            get => _selectedClaimIntimation;
            set
            {
                if (_selectedClaimIntimation != value)
                {
                    _selectedClaimIntimation = value;
                    OnPropertyChanged(nameof(SelectedClaimIntimation));
                }
            }
        }

        private string _tappedClaimIntimationCommand;
        public string TappedClaimIntimationCommand
        {
            get => _tappedClaimIntimationCommand;
            set
            {
                if (_tappedClaimIntimationCommand != value)
                {
                    _tappedClaimIntimationCommand = value;
                    OnPropertyChanged(nameof(TappedClaimIntimationCommand));
                }
            }
        }

        [ObservableProperty]
        ObservableCollection<IntimationCardModel> _getClaimIntimationPreviewList;

        public ClaimDataCardClass()
        {
            GetClaimIntimationPreviewList = new ObservableCollection<IntimationCardModel>();
        }

    }
}
