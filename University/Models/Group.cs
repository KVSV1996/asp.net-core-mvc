using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using University.Models;

namespace University.Models
{
    public class Group
    {        
        public int GroupId { get; set; }       
        public int CourseId { get; set; }        
        public string GroupName { get; set; }       
        
    }
}
