using SQLite;
using StudentCrudWithSQLite.DataBases;
using StudentCrudWithSQLite.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace StudentCrudWithSQLite.Services
{
    public class StudentDataStore : IStudentStore
    {
        private readonly SQLiteConnection _sQLiteConnection;

        public StudentDataStore()
        {
            _sQLiteConnection = DependencyService.Get<IDataBase>().GetConnection();
            _sQLiteConnection.CreateTable<Student>();
        }

        public bool NewStudent(Student student)
        {
            try
            {
                var result = _sQLiteConnection.Insert(student);
                if (result == 0) { return false; }

                return true;
            }
            catch (SQLiteException) { throw; }
        }

        public Student GetStudentBy(string id)
        {
            try
            {
                //return _sQLiteConnection.Get<Student>(id);
                return _sQLiteConnection.Query<Student>("SELECT * FROM Student Where Id=?", id).FirstOrDefault();
            }
            catch (SQLiteException) { throw; }
        }

        public bool RemoveStudent(string id)
        {
            try
            {
                var result = _sQLiteConnection.Delete<Student>(id);
                if (result == 0) { return false; }

                return true;
            }
            catch (SQLiteException) { throw; }
        }

        public bool UpdateStudent(Student student)
        {
            try
            {
                var result = _sQLiteConnection.Update(student);
                if (result == 0) { return false; }

                return true;
            }
            catch (SQLiteException) { throw; }
        }

        public IEnumerable<Student> GetStudents()
        {
            try
            {
                return _sQLiteConnection.Query<Student>("SELECT * FROM Student");
            }
            catch (SQLiteException) { throw; }
        }
    }
}