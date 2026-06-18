namespace Ums.Models
{
    public class Enrollment
    {

        public int Student_id { get; set; }

        public int Course_id { get; set; }

        public string? Grade { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
