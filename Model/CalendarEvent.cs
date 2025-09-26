namespace Avatab.Model
{


    public class CalendarEvent
    {

        public string title = "";

        public int hour;

        public int minutes = 0;

        public int duration;

        public Rect layoutBounds;

        public List<int> occpiuedId = new();
    }
}
