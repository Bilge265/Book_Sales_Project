using System.ComponentModel.DataAnnotations;

namespace Book_Sales_Project.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "E posta")]
        [Required(ErrorMessage = "E posta giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifreyi giriniz.")]
        public string Password { get; set; }

    }
}
