using University.Models;

namespace University.Services.Interface
{
    public interface IGroupService
    {
        IEnumerable<Group> IndexGroupLogic(int? id, string searchString);
        int GroupPageSize();
        void GroupDelete(int id);
        void GroupCreate(Group @group);
        void UpdateGroup(Group @group);
        IEnumerable<Group> GetAllGroups();        
        Group GetGroupById(int id);       
    }
}
