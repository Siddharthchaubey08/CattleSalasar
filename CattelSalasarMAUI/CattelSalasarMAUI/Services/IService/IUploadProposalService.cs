using CattelSalasarMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Services.IService
{
    public interface IUploadProposalService
    {
        Task<string> UpdateAnimalDetailsToServer(AnimalDataModel animalData);

        Task<string> SaveAnimalImagesDetails(IEnumerable<ProposalImages> Images);
    }
}
