using FluentValidation;
using KutuphaneCase.WebApp.Models;
using System;

namespace KutuphaneCase.WebApp.Validators
{
    /// <summary>
    /// BookViewModel sınıfı validasyon
    /// </summary>
    public class BookValidator : AbstractValidator<BookViewModel>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Kitap adı girin.");
            RuleFor(x => x.Authors).NotEmpty().WithMessage("Yazar ismi girin.");
        }
    }
}
