using Microsoft.EntityFrameworkCore;
using Ums.Data;
using Ums.Models;

namespace Ums.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly UmsContext _context;

        public CourseRepository(UmsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Department)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Course_id == id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
                _context.Courses.Remove(course);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
