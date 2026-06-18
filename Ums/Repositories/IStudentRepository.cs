using Ums.Models;
using System.Collections.Generic;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
    Task SaveAsync();
    Task<Student> GetByIdentityUserIdAsync(string identityUserId);

}
