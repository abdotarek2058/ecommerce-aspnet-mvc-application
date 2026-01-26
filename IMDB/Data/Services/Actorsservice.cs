using IMDB.Data.Base;
using IMDB.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Data.Services
{
    public class Actorsservice : EntityBaseRepositry<Actor>,IActorsService
    {
        public Actorsservice(AppDbContext context) : base(context) { }
    }
}
