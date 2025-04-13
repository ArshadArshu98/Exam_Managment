using Models;
using Models.DTOs;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStudentService
    {

        Task<CommonResponse<List<StudentDto>>> GetAllAsync();
        Task<CommonResponse<string>> AddOrUpdateAsync(StudentDto studentDto);

    }
}
