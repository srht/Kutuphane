using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Core.Entities
{
    public class Book:BaseEntity
    {
        [MaxLength(500)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Author { get; set; }
        [MaxLength(5000)]
        public string URL { get; set; }
        [MaxLength(500)]
        public string? BorrowedBy { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
