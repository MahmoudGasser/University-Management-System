
using Microsoft.AspNetCore.Mvc;

namespace Ums.Models
{
    public class Course
    {
        public int Course_id { get; set; }

       
        
        public string name { get; set; }

        

        public int CreditHours { get; set; }

        public int Department_id { get; set; }

        public Department Department { get; set; }

        public int Instructor_id { get; set; }

        public Instructor Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}
