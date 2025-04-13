using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class SubjectMarkDto
    {
        public int SubjectId { get; set; }

        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100")]
        public decimal Mark { get; set; }
        public string? SubjectName { get; set; }
    }
}
