using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace CattelSalasarMAUI.HelperModels
{
    public partial class DataCardPropClass : BaseViewModel, INotifyPropertyChanged
    {
        [ObservableProperty]
        private string _leadNumber;
        [ObservableProperty]
        private string _customerName;
        [ObservableProperty]
        private string _proposalId;
        [ObservableProperty]
        private string _customerMobile;

        [ObservableProperty]
        private string _numberOfCattleToBeInsured;

        
        private ProposalBasicDetailModel _selectedProposalTap;
        public ProposalBasicDetailModel SelectedProposalTap
        {
            get => _selectedProposalTap;
            set
            {
                if (_selectedProposalTap != value)
                {
                    _selectedProposalTap = value;
                    OnPropertyChanged(nameof(SelectedProposalTap));
                }
            }
        }

        public ObservableCollection<ProposalBasicDetailModel> GetProposalPreviewList { get; set; }
       
        //private ObservableCollection<ProposalBasicDetailModel> _getProposalPreviewList;
        //public ObservableCollection<ProposalBasicDetailModel> GetProposalPreviewList
        //{
        //    get => _getProposalPreviewList;
        //    set
        //    {
        //        if (_getProposalPreviewList != value)
        //        {
        //            _getProposalPreviewList = value;
        //            OnPropertyChanged(nameof(GetProposalPreviewList));
        //        }
        //    }
        //}
        public DataCardPropClass()
        {
            GetProposalPreviewList=new ObservableCollection<ProposalBasicDetailModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }


    }
}
