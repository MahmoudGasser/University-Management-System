using System.ComponentModel.DataAnnotations;
using Ums.Validations;

namespace Ums.Models
{
    public class Department
    {

        public int Department_id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [MinTwoCharsName]   
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();  

    }
}
