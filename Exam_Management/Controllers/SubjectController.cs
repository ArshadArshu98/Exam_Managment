using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Responses;
using Services.Interfaces;

namespace Exam_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }
        [HttpGet("GetSubjects")]
        public async Task<CommonResponse<List<SubjectDto>>> GetAllSubjects()
        {
            var response = await _service.GetAllSubjects();
            return response;
        }
    }
}
