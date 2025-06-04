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
    public partial class AnimalDataCardClass : BaseViewModel
    {
        [ObservableProperty]
        private string _typeofAnimal;
        [ObservableProperty]
        private string _tagNo;
        [ObservableProperty]
        private string _drContactName;
        [ObservableProperty]
        private string _drContactNo;

        [ObservableProperty]
        AnimalDataModel _selectedAnimalDataItems;

        [ObservableProperty]
        ObservableCollection<AnimalDataModel> _cardAnimalDetailsList;
       
        [ObservableProperty]
        ObservableCollection<ClaimAnimalCard> _claimCardAnimalDetailsList;

        public AnimalDataCardClass()
        {
            CardAnimalDetailsList = new ObservableCollection<AnimalDataModel>();
            ClaimCardAnimalDetailsList = new ObservableCollection<ClaimAnimalCard>();
        }
    }
}
