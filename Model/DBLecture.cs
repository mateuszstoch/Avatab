using SQLite;


namespace Avatab.Model
{
    public enum LectureType
    {
        wyklad = 0,
        cwiczenia = 1,
        lektorat = 2
    }
    public class DBLecture
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string profesor { get; set; }
        public LectureType lectureType { get; set; }
        public TimeSpan timeStart { get; set; }
        public TimeSpan timeEnd { get; set; }
        public DateTime date { get; set; }
        public string place { get; set; }
        public long parentId { get; set; }

        public DBLecture()
        {
            isPending = false;
        }


        [Ignore]
        public string TimeRange => $"{timeStart.ToString(@"hh\:mm")} - {timeEnd.ToString(@"hh\:mm")}";

        [Ignore]
        public bool isPending { get; set; }

    }
}
