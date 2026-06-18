using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ums.Models;
using Ums.Repositories;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class DepartmentRepositoryController : ControllerBase
{
    private readonly IDepartmentRepository _repo;

    public DepartmentRepositoryController(IDepartmentRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    

    public async Task<IActionResult> GetAll()
    {
        var deps = await _repo.GetAllAsync();
        return Ok(deps);
    }

    [HttpGet("{id}")]
    

    public async Task<IActionResult> GetById(int id)
    {
        var dep = await _repo.GetByIdAsync(id); /
        return Ok(dep);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Department department)
    {
        await _repo.AddAsync(department);
        await _repo.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = department.Department_id }, department);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Department updated)
    {
        if (id != updated.Department_id) return BadRequest("ID mismatch");

        var existing = await _repo.GetByIdAsync(id); 
        existing.Name = updated.Name;

        await _repo.UpdateAsync(existing);
        await _repo.SaveAsync();

        return Ok(new { Message = "Department updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id); 
        await _repo.SaveAsync();

        return Ok(new { Message = "Department deleted successfully" });
    }
}
