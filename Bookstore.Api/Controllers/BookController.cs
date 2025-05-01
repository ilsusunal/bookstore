using AutoMapper;
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
    private readonly IMapper _mapper;

    public BookController(BookstoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get()
    {
        var books = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToListAsync();

        var bookDtos = _mapper.Map<List<BookDto>>(books);
        return Ok(bookDtos);
    }

    // GET: api/book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return NotFound();

        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }

    // POST: api/book
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookRequest request)
    {
        var book = _mapper.Map<Book>(request);

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

        _mapper.Map(request, book); // mevcut entity'ye request'i uygula
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
