using System.ComponentModel.DataAnnotations;

namespace University.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        [Display(Name = "Group Id")]
        public int GroupId { get; set; }
        [Display(Name = "Students Name")]
        public string StudentName { get; set; }
    }
}
