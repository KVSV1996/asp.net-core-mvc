using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using University.Models;

namespace University.Data
{
    public interface IUniversityContext
    {
        DbSet<Student> Student { get; set; }

        DbSet<Group> Group { get; set; }

        DbSet<Course> Course { get; set; }

        int SaveChanges();

        EntityEntry Entry(object entity);

        void Dispose();

    }
}
