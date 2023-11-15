using Moq;
using University.Interface;
using University.Models;
using University.Services;

namespace University.Test.ServiceTests
{
    public class CourseServiceTests
    {        
        private Mock<ICourseRepository> _courseRepositoryMock = new();
        private Mock<IGroupRepository> _groupRepositoryMoc = new();

        [Fact]
        public void IndexCourseLogic_Null_Result()
        {
            _courseRepositoryMock
                .Setup(r => r.GetAllCourses())
                .Returns(GetTestCourses());

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);

            var result = service.IndexCourseLogic(null);

            var list = result.ToList();

            Assert.Equal(5, list.Count);

        }

        [Fact]
        public void IndexCourseLogic_SearchString_Result()
        {
            _courseRepositoryMock
                .Setup(r => r.GetAllCourses())
                .Returns(GetTestCourses());

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);

            var result = service.IndexCourseLogic("first");

            var list = result.ToList();

            Assert.Equal(1, list.Count);

        }

        [Fact]
        public void GetCourses_Test()
        {                     
            _courseRepositoryMock
                .Setup(r => r.GetAllCourses())
                .Returns(GetTestCourses());

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);
            
            var result = service.GetAllCourses();
            
            _courseRepositoryMock.Verify(r => r.GetAllCourses());
            Assert.Equal(5, result.Count());
        }


        [Fact]
        public void GetCourseById_Test()
        {            
            _courseRepositoryMock
                .Setup(r => r.GetCourseById(1))
                .Returns(new Course { CourseId = 1, CourseName = "The first" });

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);
            
            var result = service.GetCourseById(1);
            
            _courseRepositoryMock.Verify(r => r.GetCourseById(1));
            Assert.Equal("The first", result.CourseName);
        }

        [Fact]
        public void UpdateCourse_Test()
        {
            Course course = new Course { CourseId = 1, CourseName = "The first Update" };                         

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);
            service.UpdateCourse(course);

            _courseRepositoryMock.Verify(r => r.UpdateCourse(course));
            _courseRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public void CourseCreate_Test()
        {
            Course course = new Course { CourseId = 6, CourseName = "The Sixth" };

            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);
            service.CourseCreate(course);

            _courseRepositoryMock.Verify(r => r.InsertCourse(course));
            _courseRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public void CourseDelete_Test()
        {           
            var service = new CourseService(_courseRepositoryMock.Object, _groupRepositoryMoc.Object);

            service.CourseDelete(1);

            _courseRepositoryMock.Verify(r => r.DeleteCourse(1));            
            _courseRepositoryMock.Verify(r => r.Save());
        }

        private List<Course> GetTestCourses()
        {
            var courses = new List<Course>
            {
                new Course { CourseId=1, CourseName="The first"},
                new Course { CourseId=2, CourseName="The second"},
                new Course { CourseId=3, CourseName="The third"},
                new Course { CourseId=4, CourseName="The fourth"},
                new Course { CourseId=5, CourseName="The fifth"}
            };
            return courses;
        }
    }
}
