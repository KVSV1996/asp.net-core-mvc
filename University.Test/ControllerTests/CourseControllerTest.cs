using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SendGrid.Helpers.Errors.Model;
using University.Controllers;
using University.Models;
using University.Services.Interface;
using University.ViewModels;

namespace University.Test.ControllerTests
{
    public class CourseControllerTest
    {
        private Mock<ICourseService> _courseServiceMock = new ();
        private IMapper mapper;

        public CourseControllerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseViewModel>());
            mapper = new Mapper(config);
        }

        [Fact]
        public void Index_NullEnter_ReturnResult()
        {                        
            var controller = new CourseController(_courseServiceMock.Object, mapper);

            var result = controller.Index(null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Course>>(viewResult.Model);
           
        }

        [Fact]
        public void Index_StringEnter_ReturnResult()
        {
            var controller = new CourseController(_courseServiceMock.Object, mapper);

            var result = controller.Index("first", null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Course>>(viewResult.Model);
        }


        [Fact]
        public void CreatePost_NotNull_ReturnResult()
        {
            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.Create(new Course { CourseId = 1, CourseName = "The first" });

            _courseServiceMock.Verify(m => m.CourseCreate(It.IsAny<Course>()));
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Create_Null_ReturnResult()
        {
            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.Create();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Null_ReturnResult()
        {
            _courseServiceMock
                .Setup(r => r.GetCourseById(1))
                .Returns(new Course { CourseId = 1, CourseName = "The first" });

            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.Edit(1);

            _courseServiceMock.Verify(m => m.GetCourseById(1), Times.Once());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPost_Course_ReturnResult()
        {
            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.Edit(1, new Course { CourseId = 1, CourseName = "The firstTest" });

            _courseServiceMock.Verify(m => m.UpdateCourse(It.IsAny<Course>()));

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Delete_Null_ReturnResult()
        {         
            _courseServiceMock
                .Setup(r => r.GetCourseById(1))
                .Returns(new Course { CourseId = 1, CourseName = "The first" });

            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.Delete(1);

            _courseServiceMock.Verify(m => m.GetCourseById(1), Times.Once());
        }


        [Fact]
        public void DeleteConfirmedTest_IsExsist_True()
        {
            var controller = new CourseController(_courseServiceMock.Object, mapper);
            var result = controller.DeleteConfirmed(1);

            _courseServiceMock.Verify(m => m.CourseDelete(1), Times.Once());

            Assert.IsType<RedirectToActionResult>(result);
        }                      
    }
}