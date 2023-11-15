using SendGrid.Helpers.Errors.Model;
using University.Data;
using University.Interface;
using University.Models;
using University.Repository;
using University.Services.Interface;

namespace University.Services
{
    public class CourseService : ICourseService
    {
        
        private ICourseRepository _courseRepository;
        private IGroupRepository _groupRepository;
        public CourseService(ICourseRepository courseRepository, IGroupRepository groupRepository)
        {            
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            
        }

        public IEnumerable<Course> IndexCourseLogic(string searchString)
        {
            var course = _courseRepository.GetAllCourses();            

            if (!String.IsNullOrEmpty(searchString))
            {
                course = course.Where(s => s.CourseName!.Contains(searchString));
            }

            return course;
        }

        public int CoursePageSize()
        {
            int pageSize = 3;
            return pageSize;
        }
        public void CourseDelete(int id)
        {           
            int groupCount = _groupRepository.GetAllGroups()
                .Where(s => s.CourseId == id)
                .Count();

            if (groupCount != 0)
            {
                throw new NotFoundException();
            }

            _courseRepository.DeleteCourse(id);
            _courseRepository.Save();
        }

        public void CourseCreate(Course course)
        {            
            _courseRepository.InsertCourse(course);
            _courseRepository.Save();
        }

        public void UpdateCourse(Course course)
        {           
            _courseRepository.UpdateCourse(course);
            _courseRepository.Save();
        }

        public IEnumerable<Course> GetAllCourses() => _courseRepository.GetAllCourses();
        public Course GetCourseById(int id) => _courseRepository.GetCourseById(id);       
    }
}
