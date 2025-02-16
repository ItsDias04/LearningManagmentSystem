using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Models
{
    public class Tutor
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
        public List<Subjects>? Subjects { get; set; } = new List<Subjects>();
        
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
    }
}
