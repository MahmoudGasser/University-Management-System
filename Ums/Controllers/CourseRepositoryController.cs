using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ums.Models;
using Ums.Repositories;

namespace Ums.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CourseRepositoryController : ControllerBase
    {
        private readonly ICourseRepository _repo;

        public CourseRepositoryController(ICourseRepository repo) => _repo = repo;

    
        [HttpGet]
       
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _repo.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            await _repo.AddAsync(course);
            await _repo.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = course.Course_id }, course);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Course updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.name = updated.name;
            existing.CreditHours = updated.CreditHours;
            existing.Department_id = updated.Department_id;
            existing.Instructor_id = updated.Instructor_id;

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
