using Microsoft.EntityFrameworkCore;
using Bookstore.Api.Services.Interfaces;
using Bookstore.Domain.Entities;
using Bookstore.Infrastructure.Data;

namespace Bookstore.Api.Services.Implamentations
{
    public class GenreService : IGenreService
    {
        private readonly BookstoreDbContext _context;

        public GenreService(BookstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.Include(g => g.Books).ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.Include(g => g.Books)
                                        .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<bool> UpdateAsync(int id, Genre genre)
        {
            if (id != genre.Id) return false;

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Genres.AnyAsync(g => g.Id == id))
                    return false;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
