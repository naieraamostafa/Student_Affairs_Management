using Microsoft.EntityFrameworkCore;
using Student_Affairs.Models;

namespace Student_Affairs.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _dbContext;

        public GradeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Grade>> GetAllGradesAsync()
        {
            return await _dbContext.Grades.ToListAsync();
        }

        public async Task<Grade> GetGradeByIdAsync(int id)
        {
            return await _dbContext.Grades.FindAsync(id);
        }

        public async Task AddGradeAsync(Grade grade)
        {
            _dbContext.Grades.Add(grade);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            _dbContext.Entry(grade).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(grade.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteGradeAsync(int id)
        {
            var grade = await _dbContext.Grades.FindAsync(id);
            if (grade != null)
            {
                _dbContext.Grades.Remove(grade);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private bool GradeExists(int id)
        {
            return _dbContext.Grades.Any(e => e.Id == id);
        }
    }
}
