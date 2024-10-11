using Avatab.Constants;
using Avatab.Model;
using Avatab.Services.Interfaces;
using SQLite;

namespace Avatab.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteConnection dbConnection;
        public DatabaseService() {
            this.dbConnection = new SQLiteConnection(DatabaseConstants.DatabasePath,DatabaseConstants.Flags);

            dbConnection.CreateTable<DBLecture>();
            dbConnection.CreateTable<DBPerson>();
        }

        public void AddLecture(DBLecture lecture)
        {
            throw new NotImplementedException();
        }

        public void AddPerson(DBPerson person)
        {
            throw new NotImplementedException();
        }

        public void DeleteLecture(int lectureId)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(DBPerson person)
        {
            throw new NotImplementedException();
        }

        public List<DBLecture> GetAllDays(int parentId)
        {
            return dbConnection.Query<DBLecture>("select * from DBLecture where parentId=?", [parentId.ToString()]);
        }

        public List<DBLecture> GetAllLectures(int parentId)
        {
            throw new NotImplementedException();
        }

        public List<DBPerson> GetAllPeople()
        {
            throw new NotImplementedException();
        }

        public void UpdateLecture(int lectureId, DBLecture lecture)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(DBPerson person)
        {
            throw new NotImplementedException();
        }
    }
}
