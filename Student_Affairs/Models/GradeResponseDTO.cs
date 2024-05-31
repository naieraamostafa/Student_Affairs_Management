namespace Student_Affairs.Models
{
    public class GradeResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string GradeValue { get; set; }
    }
}
