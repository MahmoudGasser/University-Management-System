using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ums.Models;
using Ums.Repositories;

namespace Ums.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StudentsRepositoryController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStudentRepository _repo;

        public StudentsRepositoryController(UserManager<IdentityUser> userManager, IStudentRepository repo)
        {
            _userManager = userManager;
            _repo = repo;
        }

        
        [HttpGet]
        
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        

        public async Task<IActionResult> GetById(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

      
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            await _repo.AddAsync(student);
            await _repo.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = student.Student_id }, student);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Student updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FullName = updated.FullName;
            existing.Address = updated.Address;
            await _repo.UpdateAsync(existing);
            await _repo.SaveAsync();

            return Ok(new { Message = "Updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return Ok(new { Message = "Deleted successfully." });
        }
    }
}
