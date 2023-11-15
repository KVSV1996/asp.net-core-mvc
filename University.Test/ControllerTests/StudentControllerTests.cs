using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Controllers;
using University.Models;
using University.Services.Interface;
using University.ViewModels;

namespace University.Test.ControllerTests
{
    public class StudentControllerTests
    {
        private Mock<IStudentService> _studentServiceMock = new();
        private IMapper mapper;

        public StudentControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentViewModel>());
            mapper = new Mapper(config);
        }

        [Fact]
        public void Index_NullEnter_ReturnResult()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Index(null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Student>>(viewResult.Model);

        }

        [Fact]
        public void Index_StringEnter_ReturnResult()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Index(null, "Petrov", null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Student>>(viewResult.Model);
        }


        [Fact]
        public void CreatePost_NotNull_ReturnResult()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Create(new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" });

            _studentServiceMock.Verify(m => m.StudentCreate(It.IsAny<Student>()));
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Create_Null_ReturnResult()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Null_ReturnResult()
        {
            _studentServiceMock
                .Setup(r => r.GetStudentById(1))
                .Returns(new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" });

            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Edit(1);

            _studentServiceMock.Verify(m => m.GetStudentById(1), Times.Once());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPost_Course_ReturnResult()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Edit(1, new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" });

            _studentServiceMock.Verify(m => m.UpdateStudent(It.IsAny<Student>()));

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Delete_Null_ReturnResult()
        {
            _studentServiceMock
                .Setup(r => r.GetStudentById(1))
                .Returns(new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" });

            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.Delete(1);

            _studentServiceMock.Verify(m => m.GetStudentById(1), Times.Once());
        }


        [Fact]
        public void DeleteConfirmedTest_IsExsist_True()
        {
            var controller = new StudentController(_studentServiceMock.Object, mapper);

            var result = controller.DeleteConfirmed(1);

            _studentServiceMock.Verify(m => m.StudentDelete(1), Times.Once());

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
