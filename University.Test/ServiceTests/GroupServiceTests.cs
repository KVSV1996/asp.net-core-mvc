using Moq;
using University.Interface;
using University.Models;
using University.Services;

namespace University.Test.ServiceTests
{
    public class GroupServiceTests
    {
        private Mock<IStudentRepository> _studentRepositoryMock = new();
        private Mock<IGroupRepository> _groupRepositoryMoc = new();

        [Fact]
        public void IndexGroupLogic_Null_Result()
        {
            _groupRepositoryMoc
                .Setup(r => r.GetAllGroups())
                .Returns(GetTestGroups());

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);

            var result = service.IndexGroupLogic(null,null);

            var list = result.ToList();

            Assert.Equal(3, list.Count);

        }

        [Fact]
        public void IndexGroupLogic_SearchString_Result()
        {
            _groupRepositoryMoc
                .Setup(r => r.GetAllGroups())
                .Returns(GetTestGroups());

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);

            var result = service.IndexGroupLogic(null, "Tv-91");

            var list = result.ToList();

            Assert.Equal(1, list.Count);

        }

        [Fact]
        public void GetGroups_Test()
        {
            _groupRepositoryMoc
                .Setup(r => r.GetAllGroups())
                .Returns(GetTestGroups());

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);

            var result = service.GetAllGroups();

            _groupRepositoryMoc.Verify(r => r.GetAllGroups());
            Assert.Equal(3, result.Count());
        }


        [Fact]
        public void GetGroupById_Test()
        {
            _groupRepositoryMoc
                .Setup(r => r.GetGroupById(1))
                .Returns(new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" });

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);

            var result = service.GetGroupById(1);

            _groupRepositoryMoc.Verify(r => r.GetGroupById(1));
            Assert.Equal("Tv-91", result.GroupName);
        }

        [Fact]
        public void UpdateGroup_Test()
        {
            Group group = new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91 Update" };

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);
            service.UpdateGroup(group);

            _groupRepositoryMoc.Verify(r => r.UpdateGroup(group));
            _groupRepositoryMoc.Verify(r => r.Save());
        }

        [Fact]
        public void GroupCreate_Test()
        {
            Group group = new Group { GroupId = 4, CourseId = 1, GroupName = "Tv-61" };

            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);
            service.GroupCreate(group);

            _groupRepositoryMoc.Verify(r => r.InsertGroup(group));
            _groupRepositoryMoc.Verify(r => r.Save());
        }

        [Fact]
        public void GroupDelete_Test()
        {
            var service = new GroupService(_groupRepositoryMoc.Object, _studentRepositoryMock.Object);

            service.GroupDelete(1);

            _groupRepositoryMoc.Verify(r => r.DeleteGroup(1));
            _groupRepositoryMoc.Verify(r => r.Save());
        }

        private List<Group> GetTestGroups()
        {
            var groups = new List<Group>
            {
                new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" },
                new Group { GroupId = 2, CourseId = 2, GroupName = "Tv-81" },
                new Group { GroupId = 3, CourseId = 3, GroupName = "Tv-71" }
            };
            return groups;
        }
    }
}
