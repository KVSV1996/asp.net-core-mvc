using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UniversityContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<UniversityContext>>()))
            {
                if (context.Course.Any())
                {
                    return;   // DB has been seeded
                }

                if (context.Group.Any())
                {
                    return;   // DB has been seeded
                }

                if (context.Student.Any())
                {
                    return;   // DB has been seeded
                }

               context.Course.AddRange(
                    new Course
                    {
                        CourseName = "The first"
                    },

                    new Course
                    {
                        CourseName = "The second"
                    },

                    new Course
                    {
                        CourseName = "The third"
                    },

                    new Course
                    {
                        CourseName = "The fourth"
                    },

                    new Course
                    {
                        CourseName = "The fifth"
                    }

                    );

                context.SaveChanges();

                context.Group.AddRange(
                    new Group
                    {
                        GroupName = "Tv-91",
                        CourseId = 1
                    },

                    new Group
                    {
                        GroupName = "Tv-92",
                        CourseId = 1
                    },

                    new Group
                    {
                        GroupName = "Tv-83",
                        CourseId = 2
                    },

                    new Group
                    {
                        GroupName = "Tv-84",
                        CourseId = 2
                    },

                    new Group
                    {
                        GroupName = "Tv-75",
                        CourseId = 3
                    },

                    new Group
                    {
                        GroupName = "Tv-66",
                        CourseId = 4
                    }
                    );

                context.SaveChanges();
               
                context.Student.AddRange(
                    new Student
                    {
                        StudentName = "Petrov",
                        GroupId = 1
                    },

                    new Student
                    {
                        StudentName = "Danila",
                        GroupId = 1
                    },

                    new Student
                    {
                        StudentName = "Bashira",
                        GroupId = 2
                    },

                    new Student
                    {
                        StudentName = "Nikita",
                        GroupId = 2
                    },

                    new Student
                    {
                        StudentName = "Vladon",
                        GroupId = 3
                    },

                    new Student
                    {
                        StudentName = "Valeron",
                        GroupId = 3
                    },

                    new Student
                    {
                        StudentName = "Papich",
                        GroupId = 4
                    },

                    new Student
                    {
                        StudentName = "Artem",
                        GroupId = 5
                    },

                    new Student
                    {
                        StudentName = "Anton",
                        GroupId = 5
                    },

                    new Student
                    {
                        StudentName = "Grisha",
                        GroupId = 6
                    },

                    new Student
                    {
                        StudentName = "Kachok",
                        GroupId = 6
                    }

                    );
                context.SaveChanges();

            }
        }
    }
}
