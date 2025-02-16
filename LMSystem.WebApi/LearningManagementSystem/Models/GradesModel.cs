using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Models
{
    public class Grades
    {   
        public int Id { get; set; }
        public int? gradenum { get; set; }

        public Subjects? Subjects { get; set; }
        public int SubjectsId { get; set; }

        public Student? Student { get; set; }
        public int StudentId { get; set; }
    }
}
