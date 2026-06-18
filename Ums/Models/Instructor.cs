namespace Ums.Models
{
    public class Instructor
    {
        public int Instructor_id { get; set; }
        public string name { get; set; }
        public int Department_id { get; set; }
        
        public Department Department { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public string IdentityUserId { get; set; }

    }
}
