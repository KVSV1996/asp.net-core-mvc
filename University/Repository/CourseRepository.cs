using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using University.Data;
using University.Interface;
using University.Models;

namespace University.Repository
{
    public class CourseRepository : ICourseRepository, IDisposable 
    {
        private IUniversityContext context;


        public CourseRepository(IUniversityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }       

        public IEnumerable<Course> GetAllCourses()
        {
            
            if(context.Course == null)
            {
                throw new NullReferenceException();
            }

            return context.Course;
        }

        public Course GetCourseById(int id)
        {
            if (context.Course == null)
            {
                throw new NotFoundException();
            }

            return context.Course.Find(id);
        }

        public void InsertCourse(Course course)
        {
            if (course == null)
            {
                throw new NotFoundException();
            }

            context.Course.Add(course);
        }

        public void DeleteCourse(int courseID)
        {                      
            Course course = context.Course.Find(courseID);

            if (course == null)
            {
                throw new NotFoundException();
            }
            context.Course.Remove(course);
        }

        public void UpdateCourse(Course course)
        {
            if (course == null)
            {
                throw new NotFoundException();
            }
            context.Entry(course).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

