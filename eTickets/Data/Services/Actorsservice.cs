using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class Actorsservice : EntityBaseRepositry<Actor>,IActorsService
    {
        public Actorsservice(AppDbContext context) : base(context) { }
    }
}
