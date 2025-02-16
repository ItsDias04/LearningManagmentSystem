namespace LearningManagementSystem.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Subjects> Subjects { get; set; } = new List<Subjects>();
        //public List<Tutor>? Tutors { get; set; }
        public Schools? School { get; set; }
        public int SchoolId { get; set; }
    }
}
