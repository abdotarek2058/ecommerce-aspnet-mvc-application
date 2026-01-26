using IMDB.Data.Base;
using IMDB.Models;

namespace IMDB.Data.Services
{
    public class CinemasService : EntityBaseRepositry<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context) { }
    }
}
