using CattelSalasarMAUI.HelperModels;
using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.Services.IService;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.ViewModels
{
    public partial class EditAnimalCardPageViewModel : BaseViewModel, IQueryAttributable
    {

        [ObservableProperty]
        private DataCardPropClass _editAnimalCardView;

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

        private IEditProposalService _editProposalService { get; set; }
        public EditAnimalCardPageViewModel(IEditProposalService editProposalService)
        {
            _editProposalService= editProposalService;
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // For upload details page
            if (query.ContainsKey("basicAnimalDataKeys"))
            {
                var serializedModel = query["basicAnimalDataKeys"] as string;

                if (!string.IsNullOrEmpty(serializedModel))
                {
                    try
                    {
                        // Decode and deserialize the JSON string
                        //var decodedModel = Uri.UnescapeDataString(serializedModel);
                        //var dataModel = JsonConvert.DeserializeObject<DataModelPareamiter>(decodedModel);

                        //if (dataModel != null)
                        //{
                        //    // Assign values from the deserialized object
                            
                        //}
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                }
            }

        }

        [RelayCommand]
        public async Task GetCardListMethod(Object obj)
        {
            //PageLoaderEnable = true;

            //var UserId = Preferences.Get("UserId", "");
            //string selectDate = ProposalDate.Date.ToString("dd-MMM-yyyy");
            //if (selectDate == "" && selectDate == null)
            //{
            //    selectDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //}
            //List<ProposalBasicDetailModel> data = null;
            //data = await _editProposalService.GetAllProposalMethod(UserId, selectDate);
            //EditDataCardView.GetProposalPreviewList.Clear();
            //if (data != null)
            //{
            //    // GetProposalList.Clear();
            //    foreach (var item in data)
            //    {
            //        ProposalBasicDetailModel detailModel = new ProposalBasicDetailModel()
            //        {
            //            LeadNumber = item.LeadNumber,
            //            CustomerName = item.CustomerName,
            //            Propid = item.Propid,
            //            CustomerMobile = item.CustomerMobile,
            //            NumberOfCattleToBeInsured = item.NumberOfCattleToBeInsured
            //            // QCStatus = item.QCStatus
            //        };
            //        EditDataCardView.GetProposalPreviewList.Add(detailModel);
            //    }
            //    PageLoaderEnable = false;
            //}
            //else
            //{
            //    PageLoaderEnable = false;
            //    await Task.Delay(2000);
            //    await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived-" + selectDate, "ok");
            //}
        }


        [RelayCommand]
        public async Task TappedDataList(ProposalBasicDetailModel SelectedProposalTap)
        {
            try
            {
                var data = SelectedProposalTap.LeadNumber;
                //var data1 = SelectedProposalTap.Age;
                await Shell.Current.GoToAsync($"///editAnimalCardPage/editAnimalDetailsPage?uploadbasicdetailsKeys={data}");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            // await App.Current.MainPage.DisplayAlert("Alert!", "Data Not Recived..", "ok");

        }




    }
}
