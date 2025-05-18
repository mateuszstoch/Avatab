using Avatab.Model;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class PersonEditViewModel : ObservableObject, IQueryAttributable
    {
        private IDatabaseService databaseService;
        public PersonEditViewModel(IDatabaseService _databaseService)
        {
            databaseService = _databaseService;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Person = (DBPerson)query["person"];
            Name = Person!.name;
            Date = DateTime.Now.ToString().Split(" ")[0];
            Lectures = databaseService.GetLecturesOnDay(Person.Id, DateTime.Now);
            int lectureCount = Lectures.Count;
            for (int i = 0; i < lectureCount - 1; i++)
            {
                if (Lectures[i].timeEnd != Lectures[i + 1].timeStart)
                {
                    Lectures.Add(new DBLecture { Name = "Avaliable", timeStart = Lectures[i].timeEnd, timeEnd = Lectures[i + 1].timeStart, });
                }
            }
            Lectures = Lectures.OrderBy(l => TimeSpan.Parse(l.timeStart)).ToList();

        }

        [ObservableProperty]
        private List<DBLecture> lectures;

        [ObservableProperty]
        private DBPerson person;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string date;

        [RelayCommand]
        private void Edit() { }

        [RelayCommand]
        private void AddEvent() { }


    }
}
