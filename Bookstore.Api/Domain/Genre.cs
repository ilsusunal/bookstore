using System.ComponentModel.DataAnnotations;
using Bookstore.Domain.Entities;

public class Genre
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}