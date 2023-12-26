using KutuphaneCase.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        public AppDbContext _appDbContext { get; set; }
        public IBookRepository _bookRepository { get; set; }
        public UnitOfWork(AppDbContext dbContext, IBookRepository bookRepository)
        {
            _appDbContext=dbContext;
            _bookRepository=bookRepository;
        }

        public async Task Save()
        {
            try
            {
                using (var transaction=await _appDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _appDbContext.SaveChanges();
                       await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
