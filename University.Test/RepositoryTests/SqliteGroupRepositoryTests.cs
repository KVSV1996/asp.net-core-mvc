using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Models;
using University.Repository;

namespace University.Test.RepositoryTests
{
    public class SqliteGroupRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<UniversityContext> _contextOptions;

        public SqliteGroupRepositoryTests()
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
SELECT GroupId
FROM Groups;";

                viewCommand.ExecuteNonQuery();
            }
            context.AddRange(
                new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91" },
                new Group { GroupId = 2, CourseId = 2, GroupName = "Tv-81" },
                new Group { GroupId = 3, CourseId = 3, GroupName = "Tv-71" });
            context.SaveChanges();
        }

        UniversityContext CreateContext() => new UniversityContext(_contextOptions);

        public void Dispose() => _connection.Dispose();


        [Fact]
        public void GetGroupById()
        {
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            var result = repository.GetGroupById(1);

            Assert.Equal("Tv-91", result.GroupName);
        }

        [Fact]
        public void GetAllGroups()
        {
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            var result = repository.GetAllGroups();

            Assert.Collection(
                result,
                r => Assert.Equal("Tv-91", r.GroupName),
                r => Assert.Equal("Tv-81", r.GroupName),
                r => Assert.Equal("Tv-71", r.GroupName));
        }

        [Fact]
        public void InsertGroup()
        {
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            repository.InsertGroup(new Group { GroupId = 4, CourseId = 3, GroupName = "Tv-61" });

            var result = context.Group.Find(4);
            Assert.Equal("Tv-61", result.GroupName);
        }

        [Fact]
        public void UpdateGroup()
        {
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            repository.UpdateGroup(new Group { GroupId = 1, CourseId = 1, GroupName = "Tv-91 udate test" });

            var result = context.Group.Single(b => b.GroupId == 1);
            Assert.Equal("Tv-91 udate test", result.GroupName);
        }

        [Fact]
        public void DeleteGroup()
        {
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            repository.DeleteGroup(3);
            repository.Save();

            Assert.Equal(2, context.Group.Count());
        }
    }
}

