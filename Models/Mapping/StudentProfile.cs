using AutoMapper;
using Models.DTOs;
using Models.Entities;


namespace Models.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
