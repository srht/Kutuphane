using Microsoft.Extensions.FileProviders;

namespace KutuphaneCase.WebApp.Models
{
    public class BookViewModel
    {
        public string? BookId { get; set; }
        public string? Title { get; set; }
        public string? Authors { get; set; }
        public string? PictureURL { get; set; }
        public string? BorrowedBy { get; set; }
        public IFormFile? PictureFile { get; set; }
        public string? ReturnDate { get; set; }
    }
}
