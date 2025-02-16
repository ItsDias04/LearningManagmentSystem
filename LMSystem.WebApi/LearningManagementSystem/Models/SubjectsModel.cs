namespace LearningManagementSystem.Models
{
    public class Subjects
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Tutor Tutor { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Grades> Grades { get; set; } = new List<Grades>();
        //public List<Student>? Students { get; set; }
        public string? Description { get; set; }
    }
}
