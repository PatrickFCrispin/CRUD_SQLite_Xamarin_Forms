using SQLite;
using StudentCrudWithSQLite.DataBases;
using StudentCrudWithSQLite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<bool> NewStudentAsync(Student student)
        {
            try
            {
                var result = _sQLiteConnection.Insert(student);
                if (result == 0)
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (SQLiteException) { throw; }
        }

        public Task<Student> GetStudentByAsync(string id)
        {
            try
            {
                //Podemos fazer de duas formas o 'get':
                //1
                //var result = _sQLiteConnection.Get<Student>(id);

                //2
                var result = _sQLiteConnection.Query<Student>("SELECT * FROM Student Where Id=?", id).FirstOrDefault();

                return Task.FromResult(result);
            }
            catch (SQLiteException) { throw; }
        }

        public Task<bool> RemoveStudentAsync(string id)
        {
            try
            {
                var result = _sQLiteConnection.Delete<Student>(id);
                if (result == 0)
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (SQLiteException) { throw; }
        }

        public Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                var result = _sQLiteConnection.Update(student);
                if (result == 0)
                {
                    return Task.FromResult(false);
                }

                return Task.FromResult(true);
            }
            catch (SQLiteException) { throw; }
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            //var result = _sQLiteConnection.Table<Student>().ToList();
            var result = _sQLiteConnection.Query<Student>("SELECT * FROM Student");

            return await Task.FromResult(result);
        }
    }
}