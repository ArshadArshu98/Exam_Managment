using Models.Entities;
using Services.Interfaces;
using Models.Responses;
using Models.DTOs;
using Repositories.Interfaces;
using AutoMapper;

namespace Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommonResponse<List<SubjectDto>>> GetAllSubjects()
        {
            try
            {
                var subjects = await _repository.GetAllSubjects();
                var mapped = _mapper.Map<List<SubjectDto>>(subjects);
                return CommonResponse<List<SubjectDto>>.Ok(mapped);
            }
            catch (Exception ex)
            {
                return CommonResponse<List<SubjectDto>>.Fail($"Failed to fetch students: {ex.Message}");
            }
        }
    }

}
