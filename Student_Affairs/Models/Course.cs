using System.ComponentModel.DataAnnotations;

namespace Student_Affairs.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Code { get; set; }

        public string Description { get; set; }

    }
}
