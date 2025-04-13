using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Responses;
using Services.Interfaces;

namespace Exam_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }
        [HttpPost("ManageStudent")]
        public async Task<CommonResponse<StudentDto>> AddOrUpdate([FromBody] StudentDto student)
        {           
            var response = await _service.AddOrUpdateAsync(student);
            return response;
        }

        [HttpGet("GetStudents")]
        public async Task<CommonResponse<List<StudentDto>>> GetAll()
        {
            var response = await _service.GetAllAsync();
            return response;
        }

    }
}
