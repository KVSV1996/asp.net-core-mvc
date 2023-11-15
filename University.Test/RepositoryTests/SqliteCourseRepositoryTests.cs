using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Models;
using University.Repository;

namespace University.Test.RepositoryTests
{
    public class SqliteCourseRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<UniversityContext> _contextOptions;
                
        public SqliteCourseRepositoryTests()
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
SELECT CourseId
FROM Course;";

                viewCommand.ExecuteNonQuery();
            }
            context.AddRange(
                new Course { CourseId = 1, CourseName = "The first" },
                new Course { CourseId = 2, CourseName = "The second" },
                new Course { CourseId = 3, CourseName = "The third" });
            context.SaveChanges();
        }

        UniversityContext CreateContext() => new UniversityContext(_contextOptions);

        public void Dispose() => _connection.Dispose();
                

        [Fact]
        public void GetCourseById()
        {
            using var context = CreateContext();
            var repository = new CourseRepository(context);

            var result = repository.GetCourseById(1);

            Assert.Equal("The first", result.CourseName);
        }

        [Fact]
        public void GetAllCourses()
        {
            using var context = CreateContext();
            var repository = new CourseRepository(context);

            var result = repository.GetAllCourses();

            Assert.Collection(
                result,
                r => Assert.Equal("The first", r.CourseName),
                r => Assert.Equal("The second", r.CourseName),
                r => Assert.Equal("The third", r.CourseName));
        }

        [Fact]
        public void InsertCourse()
        {
            using var context = CreateContext();
            var repository = new CourseRepository(context);

            repository.InsertCourse(new Course { CourseId = 4, CourseName = "The fourth" });

            var result = context.Course.Find(4);
            Assert.Equal("The fourth", result.CourseName);
        }

        [Fact]
        public void UpdateCourse()
        {
            using var context = CreateContext();
            var repository = new CourseRepository(context);

            repository.UpdateCourse(new Course { CourseId = 1, CourseName = "The first update test" });

            var result = context.Course.Single(b => b.CourseId == 1);
            Assert.Equal("The first update test", result.CourseName);
        }

        [Fact]
        public void DeleteCourse()
        {
            using var context = CreateContext();
            var repository = new CourseRepository(context);
                        
            repository.DeleteCourse(3);
            repository.Save();
            
            Assert.Equal(2, context.Course.Count());
        }
    }
}
