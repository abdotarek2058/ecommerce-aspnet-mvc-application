using IMDB.Data.Base;
using IMDB.Data.ViewModel;
using IMDB.Models;

namespace IMDB.Data.Services
{
    public interface IMoviesService : IEntityBaseRepositry<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdowns> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovie data);
        Task UpdateMovieAsync(NewMovie data);
    }
}
