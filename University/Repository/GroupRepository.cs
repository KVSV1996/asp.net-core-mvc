using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using University.Data;
using University.Interface;
using University.Models;

namespace University.Repository
{
    public class GroupRepository : IGroupRepository, IDisposable
    {
        private IUniversityContext context;

        public GroupRepository(IUniversityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.context = context;
        }          

        public IEnumerable<Group> GetAllGroups()
        {
            if (context.Group == null)
            {
                throw new NullReferenceException();
            }
            return context.Group;
        }

        public Group GetGroupById(int? id)
        {
            if (id == null || context.Group == null)
            {
                throw new NotFoundException();
            }
            return context.Group.Find(id);
        }

        public void InsertGroup(Group group)
        {
            if (group == null)
            {
                throw new NotFoundException();
            }
            context.Group.Add(group);
        }

        public void DeleteGroup(int? groupID)
        {
            if (groupID == null)
            {
                throw new NotFoundException();
            }
            Group group = context.Group.Find(groupID);

            if (group == null)
            {
                throw new NotFoundException();
            }
            context.Group.Remove(group);
        }

        public void UpdateGroup(Group group)
        {
            if (group == null)
            {
                throw new NotFoundException();
            }
            context.Entry(group).State = EntityState.Modified;
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
