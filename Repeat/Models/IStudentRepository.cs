using System.Collections.Generic;

namespace Repeat.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);
        IEnumerable<Student> GetAllStudents();
        Student Add(Student student);
        Student Delete(int id);
        Student Update(Student updateStudent);
    }
}
