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
    public partial class CardClaimIntimationViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _leadNumber;

        private ClaimDataCardClass _claimCardView;
        public ClaimDataCardClass ClaimCardView
        {
            get => _claimCardView;
            set
            {
                if (_claimCardView != value)
                {
                    _claimCardView = value;
                    OnPropertyChanged(nameof(ClaimCardView));
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
        private bool _claimCardViewEnable;
        public bool ClaimCardViewEnable
        {
            get => _claimCardViewEnable;
            set
            {
                if (_claimCardViewEnable != value)
                {
                    _claimCardViewEnable = value;
                    OnPropertyChanged(nameof(ClaimCardViewEnable));
                }
            }
        }

        private IClaimIntimationService _claimIntimationService { get; set; }
        public CardClaimIntimationViewModel(IClaimIntimationService claimIntimationService)
        {
            _claimIntimationService = claimIntimationService;
            ClaimCardView = new ClaimDataCardClass();
           // ClaimCardViewEnable = true;
            PageLoaderEnable = false;
        }

        [RelayCommand]
        public async Task SearchIntimation()
        {
            //ClaimCardViewEnable = false;
            PageLoaderEnable = true;
            ClaimCardView.GetClaimIntimationPreviewList.Clear();
            var result = await _claimIntimationService.GetClaimProposerDetails(LeadNumber);
            if (result != null)
            {
                foreach (var item in result)
                {
                    IntimationCardModel intimationModel = new IntimationCardModel()
                    {
                        Propid = item.Propid,
                        PolicyNo = item.PolicyNo,
                        CustomerName = item.CustomerName,
                        LeadNumber = item.LeadNumber,
                        AadharNumber = item.AadharNumber,
                        CreatedDate = item.CreatedDate,
                        CustomerMobile = item.CustomerMobile,
                        SurveyDate = item.SurveyDate
                    };
                    ClaimCardView.GetClaimIntimationPreviewList.Add(intimationModel);

                }
                PageLoaderEnable = false;
                //ClaimCardViewEnable = true;
            }
        }

        [RelayCommand]
        public async Task TappedClaimIntimationCard(object obj)
        {
            try
            {
                var paramiter = obj as IntimationCardModel;
                Preferences.Set("IntimationTappedLeadNo", paramiter.LeadNumber);
                Preferences.Set("IntimationTappedProposalNo", paramiter.Propid);
                Preferences.Set("IntimationTappedMobileNo", paramiter.CustomerMobile);
               // Preferences.Set("TempClaimPropId", "");
                ClaimIntimationResponceModel data = new ClaimIntimationResponceModel
                {
                    PropId = paramiter.Propid,
                    LeadNumber = paramiter.LeadNumber,
                };
                var serializedModel = JsonConvert.SerializeObject(data);
                var encodedModel = Uri.EscapeDataString(serializedModel);
                await Shell.Current.GoToAsync($"///cardClaimIntimation/claimAnimalCardPage?claimAnimalCardKey={encodedModel}");

                
              //  await Shell.Current.GoToAsync($"//cardClaimIntimation/claimIntimationPage");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
