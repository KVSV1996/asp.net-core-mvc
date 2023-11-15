using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace University.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Display(Name = "Courses Name")]
        public string CourseName { get; set; }
    }
}
