using AutoMapper;
using Models.DTOs;
using Models.Entities;
using Models.Responses;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _repository;
        private readonly IMapper _mapper;

        public ExamService(IExamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommonResponse<List<ExamMarkDto>>> GetMarks(int studentID)
        {
            var result = await _repository.GetMarks(studentID);
            return CommonResponse<List<ExamMarkDto>>.Ok(result, "Success");

        }

        public async Task<CommonResponse<string>> SaveStudentExamAsync(ExamMarkDto examDto)
        {
            try
            {
                bool checkExamResultExist = await _repository.CheckExamResultExist(examDto.StudentId, examDto.ExamYear);
                if (!checkExamResultExist)
                {
                    var result = await _repository.SaveStudentExamAsync(examDto);

                    if (result > 0)
                        return CommonResponse<string>.Ok("Record updated successfully.");
                    else
                        return CommonResponse<string>.Fail("No changes made.");
                }
                else
                    return CommonResponse<string>.Fail("Exam result already exist.");

            }
            catch (Exception ex)
            {
                return CommonResponse<string>.Fail($"An error occurred: {ex.Message}");
            }
        }
    }
}
