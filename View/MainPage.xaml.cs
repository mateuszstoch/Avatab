using Avatab.ViewModel;

namespace Avatab
{
    public partial class MainPage : ContentPage
    {
        private CalendarViewModel _vm;
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            _vm = new CalendarViewModel();
            MyCalendar.BindingContext = _vm;
        }

    }

}
