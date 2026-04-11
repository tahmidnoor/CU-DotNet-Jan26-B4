using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly MyAppDbContext _context;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(MyAppDbContext context, ILogger<AuthorsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/authors
    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        _logger.LogInformation("GET all authors called");

        var authors = await _context.Authors.ToListAsync();
        return Ok(authors);
    }

    // GET: api/authors/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        _logger.LogInformation($"GET author with ID {id}");

        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            _logger.LogWarning($"Author with ID {id} not found");
            return NotFound();
        }

        return Ok(author);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> AddAuthor(Author author)
    {
        _logger.LogInformation("POST add author called");

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return Ok(author);
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, Author author)
    {
        if (id != author.Id)
            return BadRequest();

        _logger.LogInformation($"PUT update author {id}");

        _context.Entry(author).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(author);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        _logger.LogInformation($"DELETE author {id}");

        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            _logger.LogWarning($"Author {id} not found for deletion");
            return NotFound();
        }

        var hasBooks = await _context.Books.AnyAsync(b => b.AuthorId == id);

        if (hasBooks)
        {
            _logger.LogWarning($"Cannot delete author {id} because they have books");
            return BadRequest("Author has associated books");
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return Ok();
    }
}