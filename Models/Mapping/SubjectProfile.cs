using AutoMapper;
using Models.DTOs;
using Models.Entities;


namespace Models.Mapping
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDto>().ReverseMap();
        }
    }
}
