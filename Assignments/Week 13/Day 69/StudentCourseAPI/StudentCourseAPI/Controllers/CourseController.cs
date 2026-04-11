using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace StudentCourseAPI.Controllers
{
    [ApiController]
    [Route("api/courses")] 
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetAll()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }

     
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            return Ok(course);
        }

       
        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (course == null)
                return BadRequest();

            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Course course)
        {
            if (id != course.Id)
                return BadRequest();

            var existingCourse = _context.Courses.Find(id);

            if (existingCourse == null)
                return NotFound();

            existingCourse.Title = course.Title;
            existingCourse.Credits = course.Credits;

            _context.SaveChanges();

            return Ok(existingCourse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Ok("Course deleted successfully");
        }
    }
}