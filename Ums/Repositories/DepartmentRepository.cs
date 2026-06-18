using Microsoft.EntityFrameworkCore;
using Ums.Data;
using Ums.Exceptions;
using Ums.Models;

namespace Ums.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly UmsContext _context;

        public DepartmentRepository(UmsContext context)
        {
            _context = context;
        }

    
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

     
        public async Task<Department> GetByIdAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                throw new NotFoundException($"Department with ID {id} not found.");

            return department;
        }

        
        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await Task.CompletedTask;
        }

      
        public async Task DeleteAsync(int id)
        {
            var department = await GetByIdAsync(id); 
            _context.Departments.Remove(department);
        }

        
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
