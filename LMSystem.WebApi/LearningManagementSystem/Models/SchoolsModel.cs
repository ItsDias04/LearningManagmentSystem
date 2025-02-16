using Microsoft.AspNetCore.Identity;

namespace LearningManagementSystem.Models
{
    public class Schools
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Student> Students { get; set; } = new List<Student>();
        //public List<Tutor> Tutors { get; set; } = new List<Tutor> { };
        public List<Group> Groups { get; set; } = new List<Group> { };
        //public List<Subjects> Subjects { get; set; } = new List<Subjects> { };
        //public List<Grades> Grades { get; set; } = new List<Grades> { };
    }
}
