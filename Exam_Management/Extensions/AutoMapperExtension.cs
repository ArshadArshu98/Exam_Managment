using Models.Mapping;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Exam_Management.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<StudentProfile>();
                cfg.AddProfile<SubjectProfile>();
            });
        }
    }

}
