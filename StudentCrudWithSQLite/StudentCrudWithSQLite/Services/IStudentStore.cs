using StudentCrudWithSQLite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentCrudWithSQLite.Services
{
    public interface IStudentStore
    {
        Task<bool> NewStudentAsync(Student student);
        Task<Student> GetStudentByAsync(string id);
        Task<bool> RemoveStudentAsync(string id);
        Task<bool> UpdateStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
    }
}