using Avatab.Model;

namespace Avatab.Services.Interfaces
{
    public interface IDatabaseService
    {
        public void AddPerson(DBPerson person);
        public void AddLecture(DBLecture lecture);
        public List<DBLecture> GetLectureOnTime(int parentId, DateTime date);
        public List<DBLecture> GetLecturesOnDay(int parentId, DateTime date);
        public List<DBLecture> GetAllLectures(int parentId);
        public List<DBPerson> GetAllPeople();
        public void UpdatePerson(DBPerson person);
        public void UpdateLecture(int lectureId, DBLecture lecture);
        public void DeletePerson(long id);
        public void DeleteLecture(int lectureId);


    }
}
