using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using University.Data;
using University.Interface;
using University.Models;

namespace University.Repository
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private IUniversityContext context;

        public StudentRepository(IUniversityContext context)
        {
            if  (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }        
        
        public IEnumerable<Student> GetAllStudents()
        {
            if (context.Student == null)
            {
                throw new NullReferenceException();
            }
            return context.Student;
        }
        public Student GetStudentById(int? id)
        {
            if (id == null || context.Student == null)
            {
                throw new NotFoundException();
            }
            return context.Student.Find(id);
        }

        public void InsertStudent(Student student)
        {
            if (student == null)
            {
                throw new NotFoundException();
            }
            context.Student.Add(student);
        }

        public void DeleteStudent(int? studentID)
        {
            if (studentID == null)
            {
                throw new NotFoundException();
            }
            Student student = context.Student.Find(studentID);

            if (student == null)
            {
                throw new NotFoundException();
            }
            context.Student.Remove(student);
        }

        public void UpdateStudent(Student student)
        {
            if (student == null)
            {
                throw new NotFoundException();
            }
            context.Entry(student).State = EntityState.Modified;
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
