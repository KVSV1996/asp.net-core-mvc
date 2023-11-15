using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Data
{
    public class UniversityContext : DbContext, IUniversityContext
    {
        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;

        public DbSet<Group> Group { get; set; }

        public DbSet<Course> Course { get; set; }
    }    
}
