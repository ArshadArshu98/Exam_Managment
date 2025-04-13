using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Data;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students
                .FromSqlRaw("EXEC sp_GetAllStudents")
                .ToListAsync();
        }

        public async Task<int> AddOrUpdateAsync(Student student)
        {
            var parameters = new[]
            {
        new SqlParameter("@StudentID", student.StudentID),
        new SqlParameter("@StudentName", student.StudentName),
        new SqlParameter("@Mail", student.Mail)
            };

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_AddOrUpdateStudent @StudentID, @StudentName, @Mail", parameters);

            return rowsAffected;
        }

        public async Task<bool> CheckEmailAlreadyExist(int studentId, string email)
        {
            try
            {
                var parameters = new[]
                                {
                                  new SqlParameter("@StudentID", studentId),
                                  new SqlParameter("@Mail", email),
                                  new SqlParameter
                                  {
                                      ParameterName = "@ReturnValue",
                                      SqlDbType = SqlDbType.Int,
                                      Direction = ParameterDirection.Output
                                  }
                                 };
            await _context.Database.ExecuteSqlRawAsync(
                    "EXEC @ReturnValue = sp_CheckEmailAlreadyExist @StudentID, @Mail",
                    parameters);

                return (int)parameters[2].Value == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
