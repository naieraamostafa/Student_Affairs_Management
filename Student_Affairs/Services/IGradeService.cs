using Student_Affairs.Models;

namespace Student_Affairs.Services
{
    public interface IGradeService
    {
        Task<List<Grade>> GetAllGradesAsync();
        Task<Grade> GetGradeByIdAsync(int id);
        Task AddGradeAsync(Grade grade);
        Task<bool> UpdateGradeAsync(Grade grade);
        Task<bool> DeleteGradeAsync(int id);
    }
}
