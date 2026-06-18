using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Ums.Models
{
    public class Student
    {
        public int Student_id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Remote(action: "CheckFullName", controller: "StudentController1", ErrorMessage = "Full name already exists")]
       
        public string FullName { get; set; }
        public string IdentityUserId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
