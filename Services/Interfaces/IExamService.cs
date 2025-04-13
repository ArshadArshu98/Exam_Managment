using Models.DTOs;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IExamService
    {
        Task<CommonResponse<List<ExamMarkDto>>> GetMarks(int studentID);
        Task<CommonResponse<string>> SaveStudentExamAsync(ExamMarkDto studentDto);
    }
}
