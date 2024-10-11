using Avatab.Model;

namespace Avatab.Services.Interfaces
{
    public interface IDatabaseService
    {
        public List<DBLecture> GetAllLectures(int parentId);
        public void AddLecture(DBLecture lecture);

        public void UpdateLecture(int lectureId, DBLecture lecture);
        public void DeleteLecture(int lectureId);

        public List<DBPerson> GetAllPeople();
        public void AddPerson(DBPerson person);
        public void UpdatePerson(DBPerson person);
        public void DeletePerson(DBPerson person);


    }
}
