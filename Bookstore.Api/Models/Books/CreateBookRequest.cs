namespace Bookstore.Api.Models.Books;

public class CreateBookRequest
{
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishedDate { get; set; }

    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public int Stock { get; set; }
}