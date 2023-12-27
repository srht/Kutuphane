using FluentValidation;
using KutuphaneCase.WebApp.Models;
using System;

namespace KutuphaneCase.WebApp.Validators
{
    /// <summary>
    /// (BorrowViewModel) Ödünç verme sınıfı validasyonu 
    /// </summary>
    public class BorrowValidator : AbstractValidator<BorrowViewModel>
    {
        public BorrowValidator()
        {
            RuleFor(x => x.BorrowedBy).Length(3,500).WithMessage("Ödünç verilecek kişi adı girin.");
            RuleFor(x => x.ReturnDate).Must(BeAValidDate).WithMessage("Lütfen doğru bir tarih girin.");
        }

        private bool BeAValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}
