using Android.Icu.Util;
using Avatab.Constants;
using Avatab.Model;
using Avatab.Services.Interfaces;
using Bumptech.Glide.Load;
using SQLite;
using System.Data.Common;

namespace Avatab.Services
{
    public class DatabaseService : IDatabaseService
    {
        public DatabaseService() {
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

        public int AddPerson(DBPerson person)
        {
            int id;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                con.BeginTransaction();
                con.Insert(person);
                con.Commit();
                id = con.Query<int>("select top 1 [Id] from _tablename order by [Id] desc")[0]; 
                con.Close();
            }
            return id;
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
            List<DBLecture> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                output = con.Query<DBLecture>("select * from DBLecture where parentId=?", [parentId.ToString()]);
                con.Close() ;
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
        public List<DBLecture> GetLectures(int parentId,DateTime date)
        {
            List<DBLecture> output;
            using (SQLiteConnection con = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags))
            {
                output = con.Query<DBLecture>("select * from DBLecture where parentId=? and timeStart < ? and timeEnd > ? and date=?", [parentId.ToString(), date.TimeOfDay, date.TimeOfDay, date.Date]);
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
