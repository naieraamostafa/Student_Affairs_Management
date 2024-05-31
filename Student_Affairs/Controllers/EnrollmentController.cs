using Microsoft.AspNetCore.Mvc;
using Student_Affairs.Models;
using Student_Affairs.Services;

namespace Student_Affairs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Enrollment>>> GetAllEnrollments()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }
        [HttpPost("{userId}/{courseId}")]
        public async Task<ActionResult<EnrollmentResponseDTO>> AddEnrollment(int userId, int courseId, [FromBody] EnrollmentDTO enrollmentDTO)
        {
            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                IsEnrolled = enrollmentDTO.IsEnrolled
            };

            await _enrollmentService.AddEnrollmentAsync(enrollment);

            var responseDTO = new EnrollmentResponseDTO
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                CourseId = enrollment.CourseId,
                IsEnrolled = enrollment.IsEnrolled
            };

            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, responseDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, EnrollmentDTO enrollmentDTO)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound(new { message = "Enrollment not found" });
            }

            enrollment.IsEnrolled = enrollmentDTO.IsEnrolled;

            var success = await _enrollmentService.UpdateEnrollmentAsync(enrollment);
            if (!success)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the enrollment" });
            }

            var responseDTO = new EnrollmentResponseDTO
            {
                Id = enrollment.Id,
                UserId = enrollment.UserId,
                CourseId = enrollment.CourseId,
                IsEnrolled = enrollment.IsEnrolled
            };

            return Ok(new { message = "Enrollment updated successfully", enrollment = responseDTO });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var success = await _enrollmentService.DeleteEnrollmentAsync(id);
            if (!success)
            {
                return NotFound(new { message = "Enrollment not found" });
            }

            return Ok(new { message = "Enrollment deleted successfully" });
        }

    }
}
