// Repositories/StudentRepository.cs
using Microsoft.EntityFrameworkCore;
using Ums.Data;
using Ums.Exceptions;
using Ums.Models;
using Ums.Repositories;

namespace Ums.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UmsContext data;

        public StudentRepository(UmsContext context)
        {
            data = context;
        }


        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await data.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Department)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await data.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Department)
                .FirstOrDefaultAsync(s => s.Student_id == id);

            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            return student;
        }

        public async Task AddAsync(Student student)
        {
            await data.Students.AddAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            data.Students.Update(student);
        }

        public async Task DeleteAsync(int id)
        {
            var student = await data.Students.FindAsync(id);
            if (student != null)
                data.Students.Remove(student);
        }

        public async Task SaveAsync()
        {
            await data.SaveChangesAsync();
        }
        public async Task<Student> GetByIdentityUserIdAsync(string identityUserId)
        {
            return await data.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.IdentityUserId == identityUserId);
        }

    }
}
