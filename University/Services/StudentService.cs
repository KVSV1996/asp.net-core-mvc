using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using University.Data;
using University.Interface;
using University.Models;
using University.Repository;
using University.Services.Interface;

namespace University.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {            
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        public IEnumerable<Student> IndexStudentLogic(int? id, string searchString)
        {
            var student = _studentRepository.GetAllStudents();

            if (id != null)
            {
                student = student.Where(s => s.StudentId == id);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                student = student.Where(s => s.StudentName!.Contains(searchString));
            }

            return student;
        }

        public int StudentPageSize()
        {
            int pageSize = 5;
            return pageSize;
        }

        public void StudentDelete(int id)
        {          
            _studentRepository.DeleteStudent(id);
            _studentRepository.Save();
        }

        public void StudentCreate(Student student)
        {
            _studentRepository.InsertStudent(student);
            _studentRepository.Save();
        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.UpdateStudent(student);
            _studentRepository.Save();
        }

        public IEnumerable<Student> GetAllStudents() => _studentRepository.GetAllStudents();
        public Student GetStudentById(int id) => _studentRepository.GetStudentById(id);       
    }
}
