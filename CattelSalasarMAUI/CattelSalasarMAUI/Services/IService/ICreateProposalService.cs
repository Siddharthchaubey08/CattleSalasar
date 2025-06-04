using CattelSalasarMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface ICreateProposalService
    {
        Task<CreateProposalResult> SaveBasicDetails(ProposalBasicDetailModel ProposalBasicDetail);
    }
}
