using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SaveExamMarkDto
    {
        public int StudentId { get; set; }
        public int ExamYear { get; set; }
        public List<SubjectMarkDto>? Marks { get; set; } = new();

    }
}
