using Microsoft.AspNetCore.Mvc;
using Student_Affairs.Models;
using Student_Affairs.Services;

namespace Student_Affairs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeResponseDTO>>> GetAllGrades()
        {
            var grades = await _gradeService.GetAllGradesAsync();
            var gradeResponseDTOs = grades.Select(g => new GradeResponseDTO
            {
                Id = g.Id,
                UserId = g.UserId,
                CourseId = g.CourseId,
                GradeValue = g.GradeValue
            }).ToList();

            return Ok(gradeResponseDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeResponseDTO>> GetGradeById(int id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound(new { message = "Grade not found" });
            }

            var gradeResponseDTO = new GradeResponseDTO
            {
                Id = grade.Id,
                UserId = grade.UserId,
                CourseId = grade.CourseId,
                GradeValue = grade.GradeValue
            };

            return Ok(gradeResponseDTO);
        }

        [HttpPost("{userId}/{courseId}")]
        public async Task<ActionResult<GradeResponseDTO>> AddGrade(int userId, int courseId, [FromBody] GradeDTO gradeDTO)
        {
            var grade = new Grade
            {
                UserId = userId,
                CourseId = courseId,
                GradeValue = gradeDTO.GradeValue
            };

            await _gradeService.AddGradeAsync(grade);

            var responseDTO = new GradeResponseDTO
            {
                Id = grade.Id,
                UserId = grade.UserId,
                CourseId = grade.CourseId,
                GradeValue = grade.GradeValue
            };

            return CreatedAtAction(nameof(GetGradeById), new { id = grade.Id }, responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(int id, [FromBody] GradeDTO gradeDTO)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
            {
                return NotFound(new { message = "Grade not found" });
            }

            grade.GradeValue = gradeDTO.GradeValue;

            var success = await _gradeService.UpdateGradeAsync(grade);
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the grade" });
            }

            var responseDTO = new GradeResponseDTO
            {
                Id = grade.Id,
                UserId = grade.UserId,
                CourseId = grade.CourseId,
                GradeValue = grade.GradeValue
            };

            return Ok(new { message = "Grade updated successfully", grade = responseDTO });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var success = await _gradeService.DeleteGradeAsync(id);
            if (!success)
            {
                return NotFound(new { message = "Grade not found" });
            }

            return Ok(new { message = "Grade deleted successfully" });
        }
    }
}
