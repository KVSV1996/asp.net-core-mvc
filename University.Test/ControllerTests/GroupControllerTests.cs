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
    public class GroupControllerTests
    {
        private Mock<IGroupService> _groupServiceMock = new();
        private IMapper mapper;

        public GroupControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupViewModel>());
            mapper = new Mapper(config);
        }

        [Fact]
        public void Index_NullEnter_ReturnResult()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);

            var result = controller.Index(null,null,null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Group>>(viewResult.Model);

        }

        [Fact]
        public void Index_StringEnter_ReturnResult()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);

            var result = controller.Index(null, "Tv-91", null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<PaginatedList<Group>>(viewResult.Model);
        }


        [Fact]
        public void CreatePost_NotNull_ReturnResult()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);

            var result = controller.Create(new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" });

            _groupServiceMock.Verify(m => m.GroupCreate(It.IsAny<Group>()));
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Create_Null_ReturnResult()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Null_ReturnResult()
        {
            _groupServiceMock
                .Setup(r => r.GetGroupById(1))
                .Returns(new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" });

            var controller = new GroupController(_groupServiceMock.Object, mapper);
            var result = controller.Edit(1);

            _groupServiceMock.Verify(m => m.GetGroupById(1), Times.Once());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPost_Course_ReturnResult()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);
            var result = controller.Edit(1, new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" });

            _groupServiceMock.Verify(m => m.UpdateGroup(It.IsAny<Group>()));

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Delete_Null_ReturnResult()
        {
            _groupServiceMock
                .Setup(r => r.GetGroupById(1))
                .Returns(new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" });

            var controller = new GroupController(_groupServiceMock.Object, mapper);
            var result = controller.Delete(1);

            _groupServiceMock.Verify(m => m.GetGroupById(1), Times.Once());
        }


        [Fact]
        public void DeleteConfirmedTest_IsExsist_True()
        {
            var controller = new GroupController(_groupServiceMock.Object, mapper);

            var result = controller.DeleteConfirmed(1);

            _groupServiceMock.Verify(m => m.GroupDelete(1), Times.Once());

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
