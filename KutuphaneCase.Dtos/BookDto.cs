using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string URL { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public DateTime? ReturnDate { get; set; }
    }
}
