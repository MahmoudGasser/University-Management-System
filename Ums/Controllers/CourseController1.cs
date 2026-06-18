using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ums.Data;
using Ums.Models;

namespace Ums.Controllers
{
    public class CourseController1 : Controller
    {
        private readonly UmsContext data;

              public CourseController1(UmsContext context)
        {
            data = context;
        }

        public IActionResult GetAllData()
        {
            var courses = data.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Department)
            .Select(c => new CourseViewModel
            {
                CourseId = c.Course_id,
                CourseTitle = c.name,
                CreditHours = c.CreditHours,
                CourseInstructor = c.Instructor.name,
                CourseDepartment = c.Department.Name
            })
            .ToList();

            
            return View("all_data", courses);
        }

        public IActionResult GetById(int id)
        {
            var courseData = data.Courses
                .Where(c => c.Course_id == id)
                .Include(c => c.Instructor)
                .Include(c => c.Department)
                .Select(c => new CourseViewModel
                {
                    CourseTitle = c.name,
                    CreditHours = c.CreditHours,
                    CourseInstructor = c.Instructor.name,
                    CourseDepartment = c.Department.Name
                }).FirstOrDefault();

            if (courseData == null)
            {

                return Content("Not Found");
            }
            
            

            return View("Get_by_id",courseData);


        }


        public IActionResult done()
        {
            ViewBag.CU_state = TempData["msg_done"];
            return View("done_create_dep");
        }


        [HttpGet]
        public IActionResult create_new_department()
        {
            
           return View("create_dep");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create_new_department(Department dept)
        {
            if (ModelState.IsValid)
            {
                data.Departments.Add(dept);
                data.SaveChanges();

                TempData["msg_done"] = "Department added successfully";
                return RedirectToAction("done");
            }
            return View("create_dep", dept);
        }


        public IActionResult GetCoursesPartial()
        {
            var courses = data.Courses
                .Select(c => new CourseViewModel
                {
                    CourseTitle = c.name,
                    CourseId = c.Course_id,         
                    CreditHours = c.CreditHours,
                    CourseDepartment = c.Department != null ? c.Department.Name : "N/A",
                    CourseInstructor = c.Instructor != null ? c.Instructor.name : "N/A"
                })
                .ToList();

            return PartialView("CourseListPartial", courses);
        }






    }
}
