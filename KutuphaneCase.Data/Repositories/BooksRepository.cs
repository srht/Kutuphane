using KutuphaneCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data.Repositories
{
    public class BooksRepository : GenericRepository<Book>,IBookRepository
    {
        public BooksRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
