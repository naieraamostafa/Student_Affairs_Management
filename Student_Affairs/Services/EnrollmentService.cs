using Microsoft.EntityFrameworkCore;
using Student_Affairs.Models;

namespace Student_Affairs.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _dbContext;

        public EnrollmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _dbContext.Enrollments.ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            return await _dbContext.Enrollments.FindAsync(id);
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            _dbContext.Enrollments.Add(enrollment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _dbContext.Entry(enrollment).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(enrollment.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool EnrollmentExists(int id)
        {
            return _dbContext.Enrollments.Any(e => e.Id == id);
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _dbContext.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _dbContext.Enrollments.Remove(enrollment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
