using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }
        [Display(Name = "Groups Name")]
        public string GroupName { get; set; }
    }
}
