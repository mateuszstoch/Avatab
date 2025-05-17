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
        private int Id { get; }
        public string Name { get; set; }
        public string profesor { get; set; }
        public LectureType lectureType { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public DateTime date { get; set; }
        public string place { get; set; }
        public long parentId { get; set; }

        public DBLecture()
        {

        }


    }
}
