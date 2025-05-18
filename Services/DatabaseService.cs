
using Avatab.Constants;
using Avatab.Model;
using Avatab.Services.Interfaces;
using SQLite;


namespace Avatab.Services
{
    public class DatabaseService : IDatabaseService
    {
        public DatabaseService()
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                con.CreateTable<DBLecture>();
                con.CreateTable<DBPerson>();
                con.Close();
            }
        }

        public void AddLecture(DBLecture lecture)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                con.BeginTransaction();
                con.Insert(lecture);
                con.Commit();
                con.Close();
            }
        }

        public void AddPerson(DBPerson person)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                con.BeginTransaction();
                con.Insert(person);
                con.Commit();
                con.Close();
            }
        }

        public void DeleteLecture(int lectureId)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(long id)
        {
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                con.Query<DBLecture>("delete from DBLecture where parentId=?", [id]);
                con.Delete<DBPerson>(id);
                con.Close();
            }
        }

        public List<DBLecture> GetAllDays(int parentId)
        {
            List<DBLecture> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                output = con.Query<DBLecture>("select * from DBLecture where parentId=?", [parentId.ToString()]);
                con.Close();
            }
            return output;
        }

        public List<DBLecture> GetAllLectures(int parentId)
        {
            List<DBLecture> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                output = con.Query<DBLecture>("select * from DBLecture");
                con.Close();
            }
            return output;

        }
        public List<DBLecture> GetLectureOnTime(int parentId, DateTime date)
        {
            List<DBLecture> output;
            string time = "";
            time = date.Hour + ":" + date.Minute + ":" + date.Second;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                SQLiteCommand cmd = con.CreateCommand("select * from DBLecture where parentId=? and timeStart < ? and timeEnd > ? and date=?", parentId, time, time, date.Date.Ticks);
                output = cmd.ExecuteQuery<DBLecture>();
                con.Close();
            }
            return output;
        }
        public List<DBLecture> GetLecturesOnDay(int parentId, DateTime date)
        {
            List<DBLecture> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                SQLiteCommand cmd = con.CreateCommand("select * from DBLecture where parentId=? and date=? order by timeStart", parentId, date.Date.Ticks);
                output = cmd.ExecuteQuery<DBLecture>();
                con.Close();
            }
            return output;
        }

        public List<DBPerson> GetAllPeople()
        {
            List<DBPerson> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                output = con.Query<DBPerson>("select * from DBPerson");
                con.Close();
            }
            return output;
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
