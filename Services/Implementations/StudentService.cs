using Models.Entities;
using Services.Interfaces;
using Models.Responses;
using Models.DTOs;
using Repositories.Interfaces;
using AutoMapper;

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommonResponse<string>> AddOrUpdateAsync(StudentDto studentDto)
        {
            try
            {
                bool emailAlreadyExist =await _repository.CheckEmailAlreadyExist(studentDto.StudentID, studentDto.Mail);

                if (!emailAlreadyExist)
                {
                    var student = _mapper.Map<Student>(studentDto);
                    var result = await _repository.AddOrUpdateAsync(student);

                    if (result > 0)
                        return CommonResponse<string>.Ok("Record updated successfully.");
                    else
                        return CommonResponse<string>.Fail("No changes made to student record.");
                }
                else
                {
                    return CommonResponse<string>.Fail("Email already exist.");
                }

            }
            catch (Exception ex)
            {
                return CommonResponse<string>.Fail($"An error occurred: {ex.Message}");
            }
        }

        public async Task<CommonResponse<List<StudentDto>>> GetAllAsync()
        {
            try
            {
                var students = await _repository.GetAllAsync();
                var mapped = _mapper.Map<List<StudentDto>>(students);
                return CommonResponse<List<StudentDto>>.Ok(mapped);
            }
            catch (Exception ex)
            {
                return CommonResponse<List<StudentDto>>.Fail($"Failed to fetch students: {ex.Message}");
            }
        }
    }

}
