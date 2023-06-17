using SQLite;

namespace StudentCrudWithSQLite.DataBases
{
    public interface IDataBase
    {
        SQLiteConnection GetConnection();
    }
}