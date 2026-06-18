using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Ums.Data;
using Ums.Models;

namespace Ums.Controllers
{
    public class InstructorController1 : Controller
    {
        private readonly UmsContext data;

        public InstructorController1(UmsContext context)
        {
            data = context;
        }
        public IActionResult GetAllData()
        {
            var instructorsData = data.Instructors
         .Include(i => i.Department)
        .Include(i => i.Courses)
        .Select(i => new InstructorViewModel
       {
                 InstructorName = i.name,
                 DepartmentName = i.Department.Name,
                 Courses = i.Courses
                     .Select(c => new CourseViewModel
                     {
                         CourseTitle = c.name,
                         CreditHours = c.CreditHours
                     })
                     .ToList()
          })
             .ToList();


            return View("GetAllData", instructorsData);
        }


        public IActionResult GetById(int id)
        {
            var instructor = data.Instructors
            .Include(i => i.Department)
            .Include(i => i.Courses)
            .Where(i => i.Instructor_id == id)
            .Select(i => new InstructorViewModel
            {
                InstructorName = i.name,
                DepartmentName = i.Department.Name,
                Courses = i.Courses
                    .Select(c => new CourseViewModel
                    {
                        CourseTitle = c.name,
                        CreditHours = c.CreditHours
                    })
                    .ToList()
            })
            .FirstOrDefault();

           if (instructor == null)
                return Content("Not Found");

            return View("GetById", instructor);
        }

        public IActionResult setSession(string drName,int drId )
        {
            HttpContext.Session.SetString("drName", drName);
            HttpContext.Session.SetInt32("drID",drId);
            
            return RedirectToAction("getSession");
        }
        public IActionResult getSession()
        {
            
            ViewBag.username = HttpContext.Session.GetString("drName"); 
            ViewBag.userId = HttpContext.Session.GetInt32("drID");

            return View("showSession");
        }

        public IActionResult GetInstructorsPartial()
        {
        
            var instructors = data.Instructors
                .Include(i => i.Department)
                .Include(i => i.Courses) 
                .Select(i => new InstructorViewModel
                {
                    InstructorName = i.name,
                    DepartmentName = i.Department.Name ,
                    Courses = i.Courses.Select(c => new CourseViewModel
                    {
                        CourseTitle = c.name,
                        CreditHours = c.CreditHours
                    }).ToList()
                })
                .ToList();

            return PartialView("InstructorListPartial", instructors);
        }
     

    }
}

