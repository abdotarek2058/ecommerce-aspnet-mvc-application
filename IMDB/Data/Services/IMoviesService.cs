using IMDB.Data.Base;
using IMDB.Models;

namespace IMDB.Data.Services
{
    public interface IMoviesService : IEntityBaseRepositry<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
