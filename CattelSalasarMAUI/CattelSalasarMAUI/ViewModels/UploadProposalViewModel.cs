using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class UploadProposalViewModel : BaseViewModel
    {
        [ObservableProperty]
        private DataCardPropClass _cardViewPropClass;

        private bool _pageLoaderEnable;
        public bool PageLoaderEnable
        {
            get => _pageLoaderEnable;
            set
            {
                if (_pageLoaderEnable != value)
                {
                    _pageLoaderEnable = value;
                    OnPropertyChanged(nameof(PageLoaderEnable));
                    
                }
            }
        }
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
                    //TappedDataList(CardViewPropClass.SelectedProposal);
                }
            }
        }

        private IEditProposalService _editProposalService { get; set; }
        public UploadProposalViewModel(IEditProposalService editProposalService)
        {
            _editProposalService = editProposalService;
            CardViewPropClass = new DataCardPropClass();
            CardViewPropClass.PropertyChanged += CardViewPropClass_PropertyChanged;

            PageLoaderEnable = false;
            //CardViewPropClass.TappedDataListCommand = new RelayCommand(async (obj) => await TappedDataList(obj));
        }

        public void CardViewPropClass_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CardViewPropClass.SelectedProposalTap))
            {
                TappedDataList(CardViewPropClass.SelectedProposalTap);
            }
        }


        [RelayCommand]
        public async Task GetUploadCardListMethod()
        {
            CardViewPropClass.GetProposalPreviewList.Clear();
            // CardViewPropClass.GetProposalPreviewList = null;
            var UserId = Preferences.Get("UserId", "");
            PageLoaderEnable = true;
            
            List<ProposalBasicDetailModel> data = null;
            data = await _editProposalService.GetUploadDataProposalListMethod(UserId);
            if (data != null)
            {
                CardViewPropClass.GetProposalPreviewList.Clear();
                foreach (var item in data)
                {
                    ProposalBasicDetailModel detailModel = new ProposalBasicDetailModel()
                    {
                        LeadNumber = item.LeadNumber,
                        CustomerName = item.CustomerName,
                        ProposalId = item.Propid,
                        CustomerMobile = item.CustomerMobile,
                        NumberOfCattleToBeInsured = item.NumberOfCattleToBeInsured,
                        // QCStatus = item.QCStatus
                    };
                    CardViewPropClass.GetProposalPreviewList.Add(detailModel);
                   
                }
                PageLoaderEnable = false;
            }
            else
            {
                PageLoaderEnable = false;
                await Toast.Make("Alert!", ToastDuration.Long, 15).Show();
                // await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived..", "ok");
            }
        }

        [RelayCommand]
        public async Task TappedDataList(ProposalBasicDetailModel SelectedProposalTap)
        {
            try
            {
                var data = SelectedProposalTap.LeadNumber;
                //var data1 = SelectedProposalTap.Age;
                await Shell.Current.GoToAsync($"//uploadPreviewPage/uploadAnimalCardPage?uploadbasicdetailsKeys={data}");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            // await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived..", "ok");

        }

    }
}
