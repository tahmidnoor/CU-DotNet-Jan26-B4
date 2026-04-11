using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using Microsoft.Extensions.Logging;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly MyAppDbContext _context;
    private readonly ILogger<BooksController> _logger;

    public BooksController(MyAppDbContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        _logger.LogInformation("GET all books called");

        var books = await _context.Books.Include(b => b.Author).ToListAsync();
        return Ok(books);
    }

    // GET: api/books/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        _logger.LogInformation($"GET book with ID {id}");

        var book = await _context.Books.Include(b => b.Author)
                                       .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            _logger.LogWarning($"Book with ID {id} not found");
            return NotFound();
        }

        return Ok(book);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
    {
        _logger.LogInformation("POST add book called");

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return Ok(book);
    }

    // PUT
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        _logger.LogInformation($"PUT update book {id}");

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(book);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        _logger.LogInformation($"DELETE book {id}");

        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            _logger.LogWarning($"Book {id} not found for deletion");
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok();
    }
}