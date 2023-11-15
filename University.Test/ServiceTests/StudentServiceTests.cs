using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Interface;
using University.Models;
using University.Services;

namespace University.Test.ServiceTests
{
    public class StudentServiceTests
    {
        private Mock<IStudentRepository> _studentRepositoryMock = new();        

        [Fact]
        public void IndexGroupLogic_Null_Result()
        {
            _studentRepositoryMock
                .Setup(r => r.GetAllStudents())
                .Returns(GetTestStuents());

            var service = new StudentService( _studentRepositoryMock.Object);

            var result = service.IndexStudentLogic(null, null);

            var list = result.ToList();

            Assert.Equal(3, list.Count);

        }

        [Fact]
        public void IndexGroupLogic_SearchString_Result()
        {
            _studentRepositoryMock
                .Setup(r => r.GetAllStudents())
                .Returns(GetTestStuents());

            var service = new StudentService(_studentRepositoryMock.Object);

            var result = service.IndexStudentLogic(null, "Petrov");

            var list = result.ToList();

            Assert.Equal(1, list.Count);

        }

        [Fact]
        public void GetGroups_Test()
        {
            _studentRepositoryMock
                .Setup(r => r.GetAllStudents())
                .Returns(GetTestStuents());

            var service = new StudentService(_studentRepositoryMock.Object);
            var result = service.GetAllStudents();

            _studentRepositoryMock.Verify(r => r.GetAllStudents());
            Assert.Equal(3, result.Count());
        }


        [Fact]
        public void GetGroupById_Test()
        {
            _studentRepositoryMock
                .Setup(r => r.GetStudentById(1))
                .Returns(new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" });

            var service = new StudentService(_studentRepositoryMock.Object);

            var result = service.GetStudentById(1);

            _studentRepositoryMock.Verify(r => r.GetStudentById(1));
            Assert.Equal("Petrov", result.StudentName);
        }

        [Fact]
        public void UpdateGroup_Test()
        {
            Student student = new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov Update" };

            var service = new StudentService(_studentRepositoryMock.Object);
            service.UpdateStudent(student);

            _studentRepositoryMock.Verify(r => r.UpdateStudent(student));
            _studentRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public void GroupCreate_Test()
        {
            Student student = new Student { StudentId = 4, GroupId = 1, StudentName = "Anton" };


            var service = new StudentService(_studentRepositoryMock.Object);
            service.StudentCreate(student);

            _studentRepositoryMock.Verify(r => r.InsertStudent(student));
            _studentRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public void GroupDelete_Test()
        {
            var service = new StudentService(_studentRepositoryMock.Object);

            service.StudentDelete(1);

            _studentRepositoryMock.Verify(r => r.DeleteStudent(1));
            _studentRepositoryMock.Verify(r => r.Save());
        }

        private List<Student> GetTestStuents()
        {
            var students = new List<Student>
            {
                new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" },
                new Student { StudentId = 2, GroupId = 1, StudentName = "Nikita" },
                new Student { StudentId = 3, GroupId = 2, StudentName = "Artem" }
            };
            return students;
        }
    }
}

