using SQLite;

namespace Avatab.Model
{
    public class DBPerson
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public bool isOccupied { get; set; }


        public DBPerson() { }
    }
}
