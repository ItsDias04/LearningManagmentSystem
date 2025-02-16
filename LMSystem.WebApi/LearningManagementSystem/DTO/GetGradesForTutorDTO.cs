namespace LearningManagementSystem.DTO
{
    public class GetGradesForTutorDTO
    {
        public string studentName {  get; set; }
        public string groupName { get; set; }
        public string subjectName { get; set; }

        public int grade { get; set; }
    }
}
