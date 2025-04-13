using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<bool> CheckEmailAlreadyExist(int studentId, string email);

        Task<List<Student>> GetAllAsync();
        Task<int> AddOrUpdateAsync(Student student);
    }
}
