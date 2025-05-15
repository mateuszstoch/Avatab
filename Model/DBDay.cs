using SQLite;


namespace Avatab.Model
{
    public enum LectureType
    {
        wyklad=0,
        cwiczenia=1,
        lektorat=2
    }
    public class DBLecture
    {
        [AutoIncrement, PrimaryKey]
        private int Id { get; }
        private string Name { get; set; }
        public string profesor { get; set; }
        public LectureType lectureType { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public DateTime date { get; set; }
        public string place {  get; set; }
        public int parentId { get; set; }

        public DBLecture()
        {

        }
        public DBLecture(string _name,string _profesor, LectureType _lectureType, string _timeStart,string _timeEnd, DateTime _date, string _place, int _parentId){
            Name = _name;
            profesor = _profesor;
            lectureType = _lectureType;
            timeStart = _timeStart;
            timeEnd = _timeEnd;
            date = _date;
            place = _place;
            parentId = parentId;
        }

    }
}
