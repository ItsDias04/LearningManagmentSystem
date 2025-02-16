namespace LearningManagementSystem.DTO
{
    public class AddStudentDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public int GroupId { get; set; }
    }
}
