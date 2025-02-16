namespace LearningManagementSystem.DTO
{
    public class GetStudentsFilterDTO
    {
        public string FilterByUserName { get; set; } = "";
        public string FilterByEmail { get; set; } = "";
        public List<string> FilterByGroupName { get; set; } = new List<string>();
        public List<string> FilterBySubjectName { get; set; } = new List<string>();
    }
}
