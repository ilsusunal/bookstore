using Bookstore.Domain.Entities;

namespace Bookstore.Api.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre> CreateAsync(Genre genre);
        Task<bool> UpdateAsync(int id, Genre genre);
        Task<bool> DeleteAsync(int id);
    }
}
