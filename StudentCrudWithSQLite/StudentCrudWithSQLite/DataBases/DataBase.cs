using SQLite;
using System;
using System.IO;

namespace StudentCrudWithSQLite.DataBases
{
    public class DataBase : IDataBase
    {
        public SQLiteConnection GetConnection()
        {
            try
            {
                var dbName = "Students.db";
                var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var path = Path.Combine(dbPath, dbName);

                return new SQLiteConnection(path);
            }
            catch (SQLiteException) { throw; }
        }
    }
}