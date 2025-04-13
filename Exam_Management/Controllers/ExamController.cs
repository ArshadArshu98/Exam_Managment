using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Responses;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Exam_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _service;
        public ExamController(IExamService service)
        {
            _service = service;
        }


        [HttpPost("SaveMarks")]
        public async Task<CommonResponse<string>> SaveStudentExam([FromBody] SaveExamMarkDto dto)
        {

            var response = await _service.SaveStudentExamAsync(dto);
            return response;

        }
        [HttpGet("GetMarks")]
        public async Task<CommonResponse<List<ExamMarkDto>>> GetMarks([FromQuery] int studentId)
        {
            var response = await _service.GetMarks(studentId);
            return response;
        }

    }
}
