namespace KutuphaneCase.WebApp.Models
{
    public class BorrowViewModel
    {
        public Guid BookId { get; set; }
        public string BorrowedBy { get; set; }
        public string ReturnDate { get; set; }
    }
}
