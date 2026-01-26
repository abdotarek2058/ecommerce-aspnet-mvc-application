using IMDB.Data.Base;
using IMDB.Models;

namespace IMDB.Data.Services
{
    public class ProducersService : EntityBaseRepositry<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context) { }
    }
    
}
