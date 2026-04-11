using Microsoft.AspNetCore.Mvc;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Controllers
{
    [ApiController]
    [Route("api/enroll")]
    public class EnrollController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        
        public IActionResult Enroll([FromBody] EnrollRequest request)
        {
            // ✅ Check if already enrolled
            var exists = _context.StudentCourses
                .Any(sc => sc.StudentId == request.StudentId
                        && sc.CourseId == request.CourseId);

            if (exists)
            {
                return BadRequest("Student is already enrolled in this course.");
            }

            var enrollment = new StudentCourse
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };

            _context.StudentCourses.Add(enrollment);
            _context.SaveChanges();

            return Ok("Enrolled successfully");
        }
    }
}
