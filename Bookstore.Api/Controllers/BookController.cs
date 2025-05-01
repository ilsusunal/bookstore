using Bookstore.Api.Models.Books;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookstoreDbContext _context;

    public BookController(BookstoreDbContext context)
    {
        _context = context;
    }

    // GET: api/book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN!,
                Description = b.Description,
                Price = b.Price,
                PageCount = b.PageCount,
                PublishedDate = b.PublishedDate,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.Name
            })
            .ToListAsync();

        return Ok(books);
    }

    // GET: api/book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Where(b => b.Id == id)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN!,
                Description = b.Description,
                Price = b.Price,
                PageCount = b.PageCount,
                PublishedDate = b.PublishedDate,
                AuthorName = b.Author.Name,
                GenreName = b.Genre.Name
            })
            .FirstOrDefaultAsync();

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST: api/book
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            ISBN = request.ISBN,
            Description = request.Description,
            Price = request.Price,
            PageCount = request.PageCount,
            PublishedDate = request.PublishedDate,
            AuthorId = request.AuthorId,
            GenreId = request.GenreId,
            Stock = request.Stock
        };

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, null);
    }

    // PUT: api/book/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBookRequest request)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        book.Title = request.Title;
        book.ISBN = request.ISBN;
        book.Description = request.Description;
        book.Price = request.Price;
        book.PageCount = request.PageCount;
        book.PublishedDate = request.PublishedDate;
        book.AuthorId = request.AuthorId;
        book.GenreId = request.GenreId;
        book.Stock = request.Stock;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/book/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
