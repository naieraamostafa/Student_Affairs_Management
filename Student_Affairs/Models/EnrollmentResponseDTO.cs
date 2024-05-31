namespace Student_Affairs.Models
{
    public class EnrollmentResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool IsEnrolled { get; set; }
    }
}
