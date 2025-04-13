using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Mail { get; set; }
    }
}
