using University.Models;

namespace University.Interface
{
    public interface IStudentRepository : IDisposable
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int? studentId);
        void InsertStudent(Student student);
        void DeleteStudent(int? studentID);
        void UpdateStudent(Student student);
        void Save();
    }
}
