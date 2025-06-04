using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class EditProposalViewModel : BaseViewModel
    {
        private bool _isCalendarVisible;
        public bool IsCalendarVisible
        {
            get => _isCalendarVisible;
            set
            {
                _isCalendarVisible = value;
                OnPropertyChanged(nameof(IsCalendarVisible));
            }
        }
        private bool _isDatePickerFocused;
        public bool IsDatePickerFocused
        {
            get => _isDatePickerFocused;
            set
            {
                _isDatePickerFocused = value;
                OnPropertyChanged(nameof(IsDatePickerFocused));
            }
        }
        private string _selectedDate;
        public string SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
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

        [ObservableProperty]
        DateTime _proposalDate;
       
        private IEditProposalService _editProposalService { get; set; }
        public EditProposalViewModel(IEditProposalService editProposalService)
        {
            _editProposalService = editProposalService;
            EditDataCardView = new DataCardPropClass();
            EditDataCardView.PropertyChanged += EditDataCardView_PropertyChanged;
            ProposalDate = DateTime.Now;

           
            OpenCalendar();
        }

        public void EditDataCardView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditDataCardView.SelectedProposalTap))
            {
                TappedDataList(EditDataCardView.SelectedProposalTap);
            }
        }
      
        [RelayCommand]
        public async Task OpenCalendar()
        {
           
            IsCalendarVisible = true;
            //IsDatePickerFocused = true;
        }


        //EditPropMethod
        [RelayCommand]
        public async Task GetCardListMethod(Object obj)
        {
            PageLoaderEnable = true;
            var UserId = "";
            var selectDate = "";
            if (SelectedDate != null)
            {
                UserId = Preferences.Get("UserId", "");
                selectDate = SelectedDate;
                if (selectDate == "" && selectDate == null)
                {
                    selectDate = DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            else 
            {
                selectDate = ProposalDate.Date.ToString("dd-MMM-yyyy");
                if (selectDate == "" && selectDate == null)
                {
                    selectDate = DateTime.Now.ToString("dd-MMM-yyyy");
                }
            }
            List<ProposalBasicDetailModel> data = null;
            data = await _editProposalService.GetAllProposalMethod(UserId, selectDate);
            EditDataCardView.GetProposalPreviewList.Clear();
            if (data != null)
            {
                // GetProposalList.Clear();
                foreach (var item in data)
                {
                    ProposalBasicDetailModel detailModel = new ProposalBasicDetailModel()
                    {
                        LeadNumber = item.LeadNumber,
                        CustomerName = item.CustomerName,
                        Propid = item.Propid,
                        CustomerMobile = item.CustomerMobile,
                        NumberOfCattleToBeInsured = item.NumberOfCattleToBeInsured
                        // QCStatus = item.QCStatus
                    };
                    EditDataCardView.GetProposalPreviewList.Add(detailModel);
                }
                PageLoaderEnable = false;
            }
            else
            {
                PageLoaderEnable = false;
                await Task.Delay(2000);
                await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived-" + selectDate, "ok");
            }
        }

        //EditPropTapMethod
        [RelayCommand]
        public async Task TappedDataList(ProposalBasicDetailModel SelectedProposalTap)
        {
            try
            {
                var data = SelectedProposalTap.Propid;
                //var data1 = SelectedProposalTap.Age;
                await Shell.Current.GoToAsync($"///editPreviewPage/editProposalDetails?editbasicdetailsKeys={data}");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            // await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived..", "ok");

        }
    }
}
