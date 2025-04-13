using Models.DTOs;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IExamRepository
    {
        Task<List<ExamMarkDto>> GetMarks(int studentID);
        Task<int> SaveStudentExamAsync(SaveExamMarkDto examDto);
        Task<bool> CheckExamResultExist(int studentId, int year);

    }
}
