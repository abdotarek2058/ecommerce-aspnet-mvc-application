using IMDB.Models;

namespace IMDB.Data.ViewModel
{
    public class NewMovieDropdowns
    {
        public NewMovieDropdowns()
        {
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
            Producers = new List<Producer>();
        }
        public List<Actor> Actors { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Producer> Producers { get; set; }
    }
}
