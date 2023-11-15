using University.Models;

namespace University.Services.Interface
{
    public interface ICourseService
    {
        IEnumerable<Course> IndexCourseLogic(string searchString);
        int CoursePageSize();
        public void CourseDelete(int id);
        public void CourseCreate(Course @course);
        public void UpdateCourse(Course @course);
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int courseId);

    }
}
