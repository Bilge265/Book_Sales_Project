using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BookValidator:AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kitap adı boş olamaz.");
            RuleFor(x => x.ImageUrl1).NotEmpty().WithMessage("Resim boş olamaz.");
            RuleFor(x => x.ImageUrl2).NotEmpty().WithMessage("Resim boş olamaz.");
            RuleFor(x => x.ImageUrl3).NotEmpty().WithMessage("Resim boş olamaz.");
            RuleFor(x=>x.ImageUrl1).Must(BeValidImageExtension).WithMessage("Proje resmi sadece .jpg ve .png uzantılı olabilir");
            RuleFor(x => x.ImageUrl1).Must(BeValidImageSize).WithMessage("Proje resmi 10 MB'dan büyük olamaz");
            RuleFor(x => x.ImageUrl2).Must(BeValidImageExtension).WithMessage("Proje resmi sadece .jpg ve .png uzantılı olabilir");
            RuleFor(x => x.ImageUrl2).Must(BeValidImageSize).WithMessage("Proje resmi 10 MB'dan büyük olamaz");
            RuleFor(x => x.ImageUrl3).Must(BeValidImageExtension).WithMessage("Proje resmi sadece .jpg ve .png uzantılı olabilir");
            RuleFor(x => x.ImageUrl3).Must(BeValidImageSize).WithMessage("Proje resmi 10 MB'dan büyük olamaz");

        }
        private bool BeValidImageSize(byte[] imageData)
        {
            if (imageData == null || imageData.Length <= 0)
            {
                return false;
            }
            const int maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
            return imageData.Length <= maxFileSizeInBytes;
        }

        private bool BeValidImageExtension(byte[] imageData)
        {
            if (imageData == null || imageData.Length <= 0)
            {
                return false;
            }
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string extension = System.IO.Path.GetExtension("filename.jpg").ToLower();

            return allowedExtensions.Contains(extension);
        }

    }
}
