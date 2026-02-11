using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.ViewModel
{
    public class Register
    {
        [Display(Name ="FullName")]
        [Required(ErrorMessage ="Full Name is Required")]
        public string FullName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
