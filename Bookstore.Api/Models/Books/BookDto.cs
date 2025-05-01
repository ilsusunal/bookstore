namespace Bookstore.Api.Models.Books;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishedDate { get; set; }

    public string AuthorName { get; set; } = null!;
    public string GenreName { get; set; } = null!;
}