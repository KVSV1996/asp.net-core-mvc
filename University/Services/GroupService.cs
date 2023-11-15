using NuGet.Protocol.Core.Types;
using SendGrid.Helpers.Errors.Model;
using University.Data;
using University.Interface;
using University.Models;
using University.Repository;
using University.Services.Interface;

namespace University.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _groupRepository;
        private IStudentRepository _studentRepository;        


        public GroupService(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {            
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        }  
        
        public IEnumerable<Group> IndexGroupLogic(int? id, string searchString)
        {
            var groups = _groupRepository.GetAllGroups();
                         
            if (id != null)
            {
                groups = groups.Where(s => s.GroupId == id);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(s => s.GroupName!.Contains(searchString));
            }           

            return groups;
        }

        public int GroupPageSize()
        {
            int pageSize = 5;
            return pageSize;
        }

        public void GroupDelete(int id)
        {          
            var studentCount = _studentRepository.GetAllStudents()
                .Where(s => s.GroupId == id)
                .Count();

            if (studentCount != 0)
            {
                throw new NotFoundException();
            }

            _groupRepository.DeleteGroup(id);
            _groupRepository.Save();
        }

        public void GroupCreate(Group @group)
        {
            _groupRepository.InsertGroup(@group);
            _groupRepository.Save();
        }

        public void UpdateGroup(Group group)
        {                       
                _groupRepository.UpdateGroup(group);
                _groupRepository.Save();           
        }        

        public IEnumerable<Group> GetAllGroups() => _groupRepository.GetAllGroups();      
        public Group GetGroupById(int id) => _groupRepository.GetGroupById(id);      
    }
}
