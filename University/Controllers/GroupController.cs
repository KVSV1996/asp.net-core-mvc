using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using University.Models;
using University.Services.Interface;
using University.ViewModels;

namespace University.Controllers
{
    public class GroupController : Controller
    {                    
        private IGroupService _groupService;
        private IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {                                  
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }       

        public IActionResult Index(int? id, string searchString, int? pageNumber)
        {                           
            return View(PaginatedList<Group>.CreateAsync(_groupService.IndexGroupLogic(id, searchString), pageNumber ?? 1, _groupService.GroupPageSize()));
        }             
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupId,CourseId,GroupName")] Group @group)
        {
            if (ModelState.IsValid)
            {               
                _groupService.GroupCreate(@group);
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }
        
        public IActionResult Edit(int id)
        { 
            var group = _groupService.GetGroupById(id);
            var model = _mapper.Map<GroupViewModel>(group);
            return View(model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("GroupId,CourseId,GroupName")] Group @group)
        {           
            if (ModelState.IsValid)
            {
                _groupService.UpdateGroup(@group);
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }
        
        public IActionResult Delete(int id)
        {
            var group = _groupService.GetGroupById(id);
            var model = _mapper.Map<GroupViewModel>(group);
            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {         
            try
            {
                _groupService.GroupDelete(id);
            }
            catch (NotFoundException)
            {
                return RedirectToAction(nameof(DeleteResult));
            }
            
            return RedirectToAction(nameof(Index));
        }

        private IActionResult DeleteResult()
        {
            return View();
        }

        private bool GroupExists(int id)
        {
          return (_groupService.GetAllGroups()?.Any(e => e.GroupId == id)).GetValueOrDefault();
        }
    }
}
