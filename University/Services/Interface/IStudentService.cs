using University.Models;

namespace University.Services.Interface
{
    public interface IStudentService
    {
        IEnumerable<Student> IndexStudentLogic(int? id, string searchString);
        int StudentPageSize();
        public void StudentDelete(int id);
        public void StudentCreate(Student @student);
        public void UpdateStudent(Student @student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int studentId);
    }
}
