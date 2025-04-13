using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;

namespace Exam_Management.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamRepository, ExamRepository>();
        }
    }
}
