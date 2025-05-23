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
            Date = DateTime.Now;
        }

        [ObservableProperty]
        private List<DBLecture> lectures;

        [ObservableProperty]
        private DBPerson person;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private void Edit() { }

        [RelayCommand]
        private void Delete(DBLecture lecture)
        {
            if (lecture.Id == -1) return;
            databaseService.DeleteLecture(lecture.Id);
            Refresh();
        }

        [RelayCommand]
        private void Refresh()
        {
            IsRefreshing = true;
            Lectures = databaseService.GetLecturesOnDay(Person.Id, Date);
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            if (Lectures.Count == 0)
            {
                TimeSpan timestart = new TimeSpan(0, 0, 0);
                TimeSpan timeend = new TimeSpan(23, 59, 0);
                Lectures.Insert(0, new DBLecture { Id = -1, Name = "Avaliable", timeStart = timestart, timeEnd = timeend, isPending = Date == DateTime.Now.Date && currentTime > timestart && currentTime < timeend });
            }
            else
            {

                if (Lectures[0].timeStart != new TimeSpan(0, 0, 0))
                {

                    Lectures.Insert(0, new DBLecture { Id = -1, Name = "Avaliable", timeStart = new TimeSpan(0, 0, 0), timeEnd = Lectures[0].timeStart });
                }
                int lectureCount = Lectures.Count;
                for (int i = 0; i < lectureCount - 1; i++)
                {
                    if (Lectures[i].timeStart < currentTime && Lectures[i].timeEnd > currentTime) Lectures[i].isPending = true;
                    if (!DateTime.Equals(Lectures[i].timeEnd, Lectures[i + 1].timeStart))
                    {
                        Lectures.Add(new DBLecture { Id = -1, Name = "Avaliable", timeStart = Lectures[i].timeEnd, timeEnd = Lectures[i + 1].timeStart, isPending = Date == DateTime.Now.Date && currentTime > Lectures[i].timeEnd && currentTime < Lectures[i + 1].timeStart });
                    }
                }
                if (Lectures[lectureCount - 1].timeStart < currentTime && Lectures[lectureCount - 1].timeEnd > currentTime) Lectures[lectureCount - 1].isPending = true;
                Lectures = Lectures.OrderBy(l => l.timeStart.ToString(@"hh\:mm")).ToList();
                if (Lectures[Lectures.Count - 1].timeEnd < new TimeSpan(24, 0, 0))
                {
                    TimeSpan timeend = new TimeSpan(23, 59, 0);
                    Lectures.Add(new DBLecture { Id = -1, Name = "Avaliable", timeStart = Lectures[Lectures.Count - 1].timeEnd, timeEnd = timeend, isPending = Date == DateTime.Now.Date && Lectures[Lectures.Count - 1].timeEnd < currentTime & timeend > currentTime });
                }
            }
            IsRefreshing = false;
        }

        [RelayCommand]
        private void AddEvent() { }

        [RelayCommand]
        private void ChangeDate(string i)
        {
            Date = Date.AddDays(int.Parse(i));
        }

        partial void OnDateChanged(DateTime newValue)
        {
            if (Person is not null)
                this.Refresh();
        }

    }
}
