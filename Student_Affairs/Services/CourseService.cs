using Student_Affairs.Models;
using Microsoft.EntityFrameworkCore;

namespace Student_Affairs.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _dbContext.Courses.FindAsync(id);
        }

        public async Task AddCourseAsync(Course course)
        {
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateCourseAsync(int id, CourseDTO courseDTO)
        {
            var existingCourse = await _dbContext.Courses.FindAsync(id);

            if (existingCourse == null)
            {
                throw new ArgumentException("Course not found");
            }

            existingCourse.Title = courseDTO.Title;
            existingCourse.Description = courseDTO.Description;
            // Update other properties as needed

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
