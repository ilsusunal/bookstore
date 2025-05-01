using System.ComponentModel.DataAnnotations;
using Bookstore.Api;
using Bookstore.Domain.Entities;

public class Author
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Biography { get; set; }

    public DateTime? BirthDate { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}