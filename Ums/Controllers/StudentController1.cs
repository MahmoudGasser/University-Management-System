using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ums.Data;
using Ums.Models;

namespace Ums.Controllers
{
    public class StudentController1 : Controller
    {
        private readonly UmsContext data;

        public StudentController1(UmsContext context)
        {
            data = context;
        }
        public IActionResult GetAllData()
        {
            ViewData["Title"] = "Students";
            var students = data.Students
             .Include(s => s.Enrollments)
             .ThenInclude(e => e.Course)
             .ThenInclude(c => c.Department)
             .Select(s => new
             {
                 StudentName = s.FullName,
                 Address = s.Address,
                 Enrollments = s.Enrollments.Select(e => new
                 {
                     CourseName = e.Course.name,
                     DepartmentName = e.Course.Department.Name,
                     CreditHours = e.Course.CreditHours,
                     Grade = e.Grade
                 }).ToList()
             }).ToList();


            ViewBag.Students = students;
            
            return View("all_data");
        }
        

        public IActionResult GetById(int id)
        {
            var studentWithEnrollments = data.Students
                .Where(s => s.Student_id == id)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Department)
                .Select(s => new
                {

                    StudentName = s.FullName,
                    studentAddress=s.Address,
                    Enrollments = s.Enrollments.Select(e => new
                    {
                        CourseName = e.Course.name,
                        DepartmentName = e.Course.Department.Name,
                        Grade = e.Grade,
                        CreditHours = e.Course.CreditHours
                    }).ToList()
                })
                .FirstOrDefault();

            if (studentWithEnrollments == null)
            {
                // Redirect to all students if not found
                return NotFound();
            }

            ViewData["std_id"] = studentWithEnrollments;

            return View("get_by_id");
        }


        public IActionResult setSession(string stdName, int stdId)
        {
            HttpContext.Session.SetString("stdName", stdName);
            HttpContext.Session.SetInt32("stdId", stdId);

            return RedirectToAction("getSession");
        }
       
        public IActionResult getSession()
        {

            ViewBag.username = HttpContext.Session.GetString("stdName");
            ViewBag.userId = HttpContext.Session.GetInt32("stdId");

            return View("showSession");
        }
        
        public IActionResult SetCookie(string stdName, int stdId)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(2); // cookie duration

            Response.Cookies.Append("username", stdName, options);
            Response.Cookies.Append("UserId", stdId.ToString(), options);
            return RedirectToAction("getCookie");
        }

        
        public IActionResult getCookie()
        {
            ViewBag.cookieUserName = Request.Cookies["username"];
            ViewBag.cookieUserID = Request.Cookies["UserId"];
            return View("showCookie");
        }

        public IActionResult create_done()
        {
            ViewBag.CU_state=TempData["msg_done"];
            return View("create_done_view");
        }
        public IActionResult edit_done()
        {
            ViewBag.CU_state = TempData["msg_done"];
            return View("edit_done_view");
        }

        [HttpGet]
        public IActionResult Create()
        {
            // send list of all courses to the view
            ViewBag.Courses = data.Courses.ToList();
            return View("create_new");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                if (student != null)
                {
                    //save student first
                    data.Students.Add(student);
                    data.SaveChanges();

                    foreach (int courseId in selectedCourses)
                    {
                        Enrollment enrollment = new Enrollment
                        {
                            Course_id = courseId,
                            Student_id = student.Student_id,
                            EnrollmentDate = DateTime.Now
                        };
                        data.Enrollments.Add(enrollment);
                    }
                    data.SaveChanges();
                    TempData["msg_done"] = "Student added successfully";
                    return RedirectToAction("create_done"); // or wherever you want
                }
                ViewBag.Courses = data.Courses.ToList();
                return View("create_new");
            }
            ViewBag.Courses = data.Courses.ToList();
            return View("create_new");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = data.Students
                                 .Include(s => s.Enrollments)
                                 .ThenInclude(e => e.Course)
                                 .FirstOrDefault(s => s.Student_id == id);

            if (student == null) return NotFound();

            ViewBag.AllCourses = data.Courses.ToList();
            return View("edit_view",student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student editedStudent, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                var student = data.Students
                                     .Include(s => s.Enrollments)
                                     .FirstOrDefault(s => s.Student_id == id);

                if (student == null) return NotFound();


                student.FullName = editedStudent.FullName;
                student.Address = editedStudent.Address;

                // delete old enroll
                var oldEnrollments = data.Enrollments.Where(e => e.Student_id == id).ToList();
                data.Enrollments.RemoveRange(oldEnrollments);

                // add new enroll
                foreach (int courseId in selectedCourses)
                {
                    data.Enrollments.Add(new Enrollment
                    {
                        Course_id = courseId,
                        Student_id = id,
                        EnrollmentDate = DateTime.Now
                    });
                }

                data.SaveChanges();

                TempData["msg_done"] = "Student updated successfully";
                return RedirectToAction("edit_done");
            }

            
            ViewBag.AllCourses = data.Courses.ToList();
            return View("edit_view", editedStudent);
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckFullName(string fullName)
        {
            bool exists = data.Students.Any(s => s.FullName == fullName);
            if (exists)
                return Json(false);

            return Json(true);
        }

        public IActionResult GetStudentsPartial()
        {
            var students = data.Students.ToList();
            return PartialView("StudentListPartial", students);
        }

       

    }
}
