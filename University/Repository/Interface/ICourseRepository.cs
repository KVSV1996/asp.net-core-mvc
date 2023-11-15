using University.Models;


namespace University.Interface
{
    public interface ICourseRepository : IDisposable
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        void InsertCourse(Course course);
        void DeleteCourse(int courseID);
        void UpdateCourse(Course course);
        void Save();
    }
}
