using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using University.Models;
using University.Services.Interface;
using University.ViewModels;

namespace University.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IMapper _mapper; 

        public CourseController(ICourseService courseService, IMapper mapper)
        {            
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
       
        public IActionResult Index(string searchString, int? pageNumber)
        {            
            return View(PaginatedList<Course>.CreateAsync(_courseService.IndexCourseLogic(searchString), pageNumber ?? 1, _courseService.CoursePageSize()));
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CourseId,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.CourseCreate(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        
        public IActionResult Edit(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Course, CourseViewModel>());
            var map = new Mapper(config);

            var course = _courseService.GetCourseById(id);
            var model = _mapper.Map<CourseViewModel>(course);
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CourseId,CourseName")] Course course)
        {           
            if (ModelState.IsValid)
            {
                _courseService.UpdateCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }  
        
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourseById(id);
            var model = _mapper.Map<CourseViewModel>(course);
            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _courseService.CourseDelete(id);
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

        private bool CourseExists(int id)
        {
            return (_courseService.GetAllCourses()?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
