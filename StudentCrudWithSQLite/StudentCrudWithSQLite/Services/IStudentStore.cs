using StudentCrudWithSQLite.Models;
using System.Collections.Generic;

namespace StudentCrudWithSQLite.Services
{
    public interface IStudentStore
    {
        bool NewStudent(Student student);
        Student GetStudentBy(string id);
        bool RemoveStudent(string id);
        bool UpdateStudent(Student student);
        IEnumerable<Student> GetStudents();
    }
}