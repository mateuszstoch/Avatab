using SQLite;


namespace Avatab.Model
{
    public enum lectureFrequency
    {
        everyWeek=0,
        oddWeeks=1,
        evenWeeks=2
    }

    public enum weekDay
    {
        sunday=0,
        monday=1,
        tuesday=2,
        wednesday=3,
        thursday=4,
        friday=5,
        saturday=6,
    }

    public class DBLecture
    {
        [AutoIncrement, PrimaryKey]
        private int Id { get; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public lectureFrequency frequency { get; set; }
        public weekDay weekDay { get; set; }
        public int parentId { get; set; }

        public DBLecture(){}

    }
}
