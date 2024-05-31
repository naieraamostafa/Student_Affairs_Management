using Student_Affairs.Models;

namespace Student_Affairs.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(int id, CourseDTO courseDTO);
        Task DeleteCourseAsync(int id);
    }
}
