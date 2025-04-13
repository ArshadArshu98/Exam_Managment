using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
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
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExamMarkDto>> GetMarks(int studentID)
        {

            try
            {
                var results = new Dictionary<int, ExamMarkDto>();

                var connection = _context.Database.GetDbConnection();
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "dbo.GetExamMarks";
                command.CommandType = CommandType.StoredProcedure;

                var param = new SqlParameter("@StudentID", SqlDbType.Int)
                {
                    Value = (object?)studentID ?? DBNull.Value
                };
                command.Parameters.Add(param);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var masterId = reader.GetInt32(0);

                    if (!results.ContainsKey(masterId))
                    {
                        results[masterId] = new ExamMarkDto
                        {

                            StudentId = reader.GetInt32(1),
                            StudentName = reader.GetString(2),
                            ExamYear = reader.GetInt32(3),
                            TotalMark = reader.GetDecimal(4),
                            PassOrFail = reader.GetBoolean(5)
                        };
                    }

                    results[masterId].Marks.Add(new SubjectMarkDto
                    {
                        SubjectId = reader.GetInt32(6),
                        SubjectName = reader.GetString(7),
                        Mark = reader.GetDecimal(8)
                    });
                }

                return results.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching student exam result.", ex);

            }
        }

        public async Task<int> SaveStudentExamAsync(ExamMarkDto dto)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "dbo.SaveStudentExam";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@StudentID", dto.StudentId));
                command.Parameters.Add(new SqlParameter("@ExamYear", dto.ExamYear));

                var dt = CreateExamDetailDataTable(dto.Marks);
                var marksParam = new SqlParameter("@Marks", dt)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.ExamDetailType"
                };
                command.Parameters.Add(marksParam);

                var result = await command.ExecuteReaderAsync();
                int masterId = 0;
                if (await result.ReadAsync())
                {
                    masterId = result.GetInt32(0);
                }
                return masterId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving student exam result.", ex);
            }
        }
        public async Task<bool> CheckExamResultExist(int studentId, int examYear)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "dbo.CheckExamResultExist";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@StudentID", studentId));
                command.Parameters.Add(new SqlParameter("@ExamYear", examYear));

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var existsFlag = reader.GetInt32(0);
                    return existsFlag == 1;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing CheckExamResultExist.", ex);

            }
            return false;
        }

        private DataTable CreateExamDetailDataTable(List<SubjectMarkDto> marks)
        {
            var table = new DataTable();
            table.Columns.Add("SubjectID", typeof(int));
            table.Columns.Add("Marks", typeof(decimal));

            foreach (var mark in marks)
            {
                table.Rows.Add(mark.SubjectId, mark.Mark);
            }

            return table;
        }

    }

}
