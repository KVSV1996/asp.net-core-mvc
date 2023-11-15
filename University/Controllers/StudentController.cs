using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Services.Interface;
using University.ViewModels;

namespace University.Controllers
{
    public class StudentController : Controller
    {                   
        private IStudentService _studentService;
        private IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public IActionResult Index(int? id, string searchString, int? pageNumber)
        {            
            return View(PaginatedList<Student>.CreateAsync(_studentService.IndexStudentLogic(id, searchString), pageNumber ?? 1, _studentService.StudentPageSize()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StudentId,GroupId,StudentName")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.StudentCreate(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            var student = _studentService.GetStudentById(id);
            var model = _mapper.Map<StudentViewModel>(student);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StudentId,GroupId,StudentName")] Student student)
        {           

            if (ModelState.IsValid)
            {
                _studentService.UpdateStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudentById(id);
            var model = _mapper.Map<StudentViewModel>(student);
            return View(model);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _studentService.StudentDelete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_studentService.GetAllStudents()?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
