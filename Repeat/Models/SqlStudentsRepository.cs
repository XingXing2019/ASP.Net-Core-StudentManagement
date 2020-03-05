using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repeat.Models
{
    public class SqlStudentsRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public SqlStudentsRepository(AppDbContext context)
        {
            _context = context;
        }
        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            var student = _context.Students.Find(id);
            if(student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public Student Update(Student updateStudent)
        {
            _context.Update(updateStudent);
            _context.SaveChanges();
            return updateStudent;
        }
    }
}
