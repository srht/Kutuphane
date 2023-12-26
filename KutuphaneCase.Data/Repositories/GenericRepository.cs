using KutuphaneCase.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data.Repositories
{
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : BaseEntity
    {
        private AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(IEntity entity)
        {
            await _dbContext.Set<IEntity>().AddAsync(entity);
        }

        public IQueryable<IEntity> GetAll()
        {
            return _dbContext.Set<IEntity>().AsNoTracking();
        }

        public async Task<IEntity> GetAsync(Guid id)
        {
            var returnedVal=await _dbContext.Set<IEntity>().AsNoTracking().FirstOrDefaultAsync(i=>i.Id== id);
            return returnedVal;
        }

        public async Task UpdateAsync(Guid id, IEntity entity)
        {
            _dbContext.Entry(entity).State= EntityState.Modified;
        }
    }
}
