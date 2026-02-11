using Microsoft.AspNetCore.Identity;

namespace IMDB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
