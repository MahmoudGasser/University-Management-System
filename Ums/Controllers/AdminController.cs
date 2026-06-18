using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ums.Data;
using Ums.Models;

namespace Ums.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UmsContext _context;

        public AdminController(UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               UmsContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

     
        public IActionResult CreateAccount()
        {
            ViewBag.Roles = new[] { "Student", "Instructor" };
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAccount(string username, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                ViewBag.Roles = new[] { "Student", "Instructor" };
                return View();
            }

           
            if (await _userManager.FindByNameAsync(username) != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                ViewBag.Roles = new[] { "Student", "Instructor" };
                return View();
            }

         
            var allowedRoles = new[] { "Student", "Instructor" };
            if (!allowedRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Role must be either 'Student' or 'Instructor'.");
                ViewBag.Roles = allowedRoles;
                return View();
            }

            var user = new IdentityUser { UserName = username, Email = username };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewBag.Roles = allowedRoles;
                return View();
            }

          
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);

           
            if (role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                var student = new Student
                {
                    FullName = username,
                    IdentityUserId = user.Id
                };
                _context.Students.Add(student);
            }
            else if (role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                var instructor = new Instructor
                {
                    name = username,
                    IdentityUserId = user.Id
                };
                _context.Instructors.Add(instructor);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }

        
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new List<(IdentityUser User, IList<string> Roles)>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add((user, roles));
            }

            return View(userRoles);
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            
            var student = _context.Students.FirstOrDefault(s => s.IdentityUserId == userId);
            if (student != null) _context.Students.Remove(student);

            var instructor = _context.Instructors.FirstOrDefault(i => i.IdentityUserId == userId);
            if (instructor != null) _context.Instructors.Remove(instructor);

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }
    }
}
