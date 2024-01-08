using backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repository
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task<int> AddMovieAsync(Movie movie);
        Task<bool> UpdateMovieAsync(int id, Movie movie);
        Task<bool> DeleteMovieAsync(int id);

    }
}
