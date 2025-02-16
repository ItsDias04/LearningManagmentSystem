using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Models
{
    public class Student
    {
        public int Id {  get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email {  get; set; }

        //public List<Subjects>? Subjects { get; set; }

        public List<Grades>? Grades { get; set; } = new List<Grades>();

        public Group? Group { get; set; } 
        public int? GroupId { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        
    }
}
