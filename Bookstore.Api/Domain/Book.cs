using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; } = null!;

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; } = null!;

        [MaxLength(13)]
        public string? ISBN { get; set; }

        public string? Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public int PageCount { get; set; }

        public int Stock { get; set; } = 0;
    }
}