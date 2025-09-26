using Avatab.ViewModel;

namespace Avatab
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm, CalendarViewModel cvm)
        {
            InitializeComponent();
            BindingContext = vm;
            MyCalendar.BindingContext = cvm;
        }

    }

}
