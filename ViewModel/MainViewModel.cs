using Avatab.Model;
using Avatab.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avatab.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private CalendarViewModel cvm;
        private IDatabaseService databaseService;

        [ObservableProperty]
        private List<DBLecture> lectures;

        public MainViewModel(CalendarViewModel _cvm, IDatabaseService _databaseService)
        {
            this.cvm = _cvm;
            this.databaseService = _databaseService;

            lectures = databaseService.GetLecturesOnDay(0, DateTime.Now);
            foreach (DBLecture lecture in lectures)
            {
                cvm.Events.Add(new CalendarEvent { title = lecture.Name, hour = lecture.date.Hour, minutes = lecture.date.Minute, duration = lecture.duration });
            }
        }

        [RelayCommand]
        public void AddEvent()
        {

        }
        [RelayCommand]
        public void Settings()
        {

        }
    }
}
