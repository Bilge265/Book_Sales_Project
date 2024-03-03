using System.ComponentModel.DataAnnotations;

namespace Book_Sales_Project.Models
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Mevcut şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre(tekrar) alanı boş bırakılamaz.")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
