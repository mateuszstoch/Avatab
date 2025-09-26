using Avatab.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;



namespace Avatab.ViewModel
{

    public partial class CalendarViewModel : ObservableObject
    {
        public ObservableCollection<CalendarEvent> Events { get; } = new();

        public CalendarViewModel()
        {

        }

        public void UpdateEventPositions(double layoutHeight, double layoutWidth)
        {
            double hourHeight = layoutHeight / 24.0;
            foreach (var ev in Events)
            {
                ev.layoutBounds = new Rect(0, (ev.hour + ev.minutes / 60.0) * hourHeight, layoutWidth, ev.duration / 60.0 * hourHeight);
            }
        }
    }
}
