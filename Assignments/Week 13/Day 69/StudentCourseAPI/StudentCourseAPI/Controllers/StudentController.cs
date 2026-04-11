using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var students = _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.Email,
                s.Age,
                Courses = s.StudentCourses
                            .Select(sc => sc.Course.Title)
                            .ToList()
            })
            .ToList();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _context.Entry(student).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        _context.SaveChanges();
        return Ok();
    }
}