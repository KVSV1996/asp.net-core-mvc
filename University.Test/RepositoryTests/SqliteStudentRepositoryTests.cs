using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using University.Data;
using University.Models;
using University.Repository;

namespace University.Test.RepositoryTests
{
    public class SqliteStudentRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<UniversityContext> _contextOptions;

        public SqliteStudentRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<UniversityContext>()
                .UseSqlite(_connection)
                .Options;


            using var context = new UniversityContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT StudentId
FROM Student;";

                viewCommand.ExecuteNonQuery();
            }
            context.AddRange(
                new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov" },
                new Student { StudentId = 2, GroupId = 1, StudentName = "Nikita" },
                new Student { StudentId = 3, GroupId = 2, StudentName = "Artem" });
            context.SaveChanges();
        }

        UniversityContext CreateContext() => new UniversityContext(_contextOptions);

        public void Dispose() => _connection.Dispose();


        [Fact]
        public void GetStudentById()
        {
            using var context = CreateContext();
            var repository = new StudentRepository(context);

            var result = repository.GetStudentById(1);

            Assert.Equal("Petrov", result.StudentName);
        }

        [Fact]
        public void GetAllStudents()
        {
            using var context = CreateContext();
            var repository = new StudentRepository(context);

            var result = repository.GetAllStudents();

            Assert.Collection(
                result,
                r => Assert.Equal("Petrov", r.StudentName),
                r => Assert.Equal("Nikita", r.StudentName),
                r => Assert.Equal("Artem", r.StudentName));
        }

        [Fact]
        public void InsertStudent()
        {
            using var context = CreateContext();
            var repository = new StudentRepository(context);

            repository.InsertStudent(new Student { StudentId = 4, GroupId = 2, StudentName = "Grisha" });

            var result = context.Student.Find(4);
            Assert.Equal("Grisha", result.StudentName);
        }

        [Fact]
        public void UpdateStudent()
        {
            using var context = CreateContext();
            var repository = new StudentRepository(context);

            repository.UpdateStudent(new Student { StudentId = 1, GroupId = 1, StudentName = "Petrov update test" });

            var result = context.Student.Single(b => b.StudentId == 1);
            Assert.Equal("Petrov update test", result.StudentName);
        }

        [Fact]
        public void DeleteStudent()
        {
            using var context = CreateContext();
            var repository = new StudentRepository(context);

            repository.DeleteStudent(3);
            repository.Save();

            Assert.Equal(2, context.Student.Count());
        }
    }
}

