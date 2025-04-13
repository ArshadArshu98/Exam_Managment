using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ExamMarkDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ExamYear { get; set; }
        public decimal TotalMark { get; set; }
        public bool PassOrFail { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<SubjectMarkDto> Marks { get; set; } = new();

    }
}
