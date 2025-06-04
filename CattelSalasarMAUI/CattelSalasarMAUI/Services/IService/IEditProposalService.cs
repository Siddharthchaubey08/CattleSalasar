using CattelSalasarMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface IEditProposalService
    {
        Task<List<ProposalBasicDetailModel>> GetAllProposalMethod(string UserId, string SelectDate);
        Task<List<ProposalBasicDetailModel>> GetUploadDataProposalListMethod(string UserId);
        Task<EditProposalModel> GetEditProposalList(string SelectPropId);

        Task<CreateProposalResult> SaveEditProposalBasicDetails(ProposalBasicDetailModel basicDetailModel1);

        Task<string> EditAnimalDetailsSave(AnimalDataModel editanimalData);

    }
}
