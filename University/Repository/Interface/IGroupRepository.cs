using System.Linq.Expressions;
using University.Models;

namespace University.Interface
{
    public interface IGroupRepository : IDisposable
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(int? groupId);
        void InsertGroup(Group group);
        void DeleteGroup(int? groupID);
        void UpdateGroup(Group group);
        void Save();
    }
}
