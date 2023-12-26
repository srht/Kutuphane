using KutuphaneCase.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data
{
    public interface IUnitOfWork:IDisposable
    {
        IBookRepository Books { get; } 
        public Task Save();
    }
}
