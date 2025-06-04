using CattelSalasarMAUI.Models;
using CattelSalasarMAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface IClaimIntimationService
    {
        Task<List<IntimationCardModel>> GetClaimProposerDetails(string SearchLeadNo);
        Task<List<ClaimAnimalCard>> GetClaimAnimalCardList(string SelectPropId);
        Task<ClaimIntimationBasic> GetBasicClaimIntimationByLeadId(string LeadNumber, string CattleNo);

        //SaveClaimIntimation on Server
        Task<ClaimIntimationResponceModel> SaveClaimIntimationDetailsOnServer(ClaimIntimationModel claimIntimation);  

        //Upload-ClaimCard List Data Get only
        Task<List<UploadClaimCardModel>> GetUpdateClaimIntimationCardList();   

        //Claim Image Save into server
        Task<bool> SaveClaimIntimationImagesOnServer(ObservableCollection<ClaimIntimationImageModel> ClaimImageDetails); 
    }
}
