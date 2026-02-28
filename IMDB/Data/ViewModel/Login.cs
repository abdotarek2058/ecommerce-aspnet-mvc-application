using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.ViewModel
{
    public class Login
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
