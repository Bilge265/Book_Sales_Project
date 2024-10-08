﻿using System.ComponentModel.DataAnnotations;

namespace Book_Sales_Project.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen mail giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi giriniz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen tekrar şifreyi giriniz.")]
        [Compare("Password", ErrorMessage = "Şifreler uyumlu değil.")]
        public string RepeatPassword { get; set; }
        public string? UserName { get; set; }

    }

}
