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
            //check who is occupied
        }

    }
}
