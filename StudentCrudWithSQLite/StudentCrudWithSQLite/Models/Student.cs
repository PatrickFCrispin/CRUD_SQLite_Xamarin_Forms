using SQLite;

namespace StudentCrudWithSQLite.Models
{
    public class Student
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}