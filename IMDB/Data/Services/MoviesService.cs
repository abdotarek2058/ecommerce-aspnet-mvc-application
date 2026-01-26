using IMDB.Data.Base;
using IMDB.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Data.Services
{
    public class MoviesService : EntityBaseRepositry<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Movie> GetMovieByIdAsync(int id)
        {
            //var movie = GetByIdAsync(id, m => m.Cinema, m => m.Producer, m => m.Actors_Movies!.ThenInclude(am => am.Actor));
            // return movie;
            var movieDetails = _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }
    }
}
