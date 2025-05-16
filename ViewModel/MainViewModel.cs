using Avatab.Model;
using Avatab.Services;
using Avatab.Services.Interfaces;
using Avatab.View;
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
                if (databaseService.GetLectures(person.Id, DateTime.Now).Count > 0)
                {
                    person.isOccupied = true;
                }
                else
                {
                    person.isOccupied = false;
                }
            }

        }

        [RelayCommand]
        public void import()
        {

        }

    }
}
