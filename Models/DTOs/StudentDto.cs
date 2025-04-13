using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Student name is required")]
        [MinLength(5, ErrorMessage = "Student name must be at least 5 characters")]
        [MaxLength(250, ErrorMessage = "Student name must not exceed 250 characters")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Mail { get; set; }
    }

}
