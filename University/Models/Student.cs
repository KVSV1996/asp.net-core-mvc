using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }       
        public string StudentName { get; set; }        
        
    }
}
