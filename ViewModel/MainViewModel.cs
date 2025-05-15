using Avatab.Model;
using Avatab.Services;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class MainViewModel(IDatabaseService databaseService) : ObservableObject
    {
        [ObservableProperty]
        private List<DBPerson> dBPeople;
        private IDatabaseService databaseService = databaseService;


        [RelayCommand]
        public void refresh()
        {
            DBPeople = databaseService.GetAllPeople();
            foreach(DBPerson person in DBPeople)
            {
                
            }

        }

        [RelayCommand]
        public void import()
        {
            
        }

    }
}
