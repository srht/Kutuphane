using KutuphaneCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data.Repositories
{
    public interface IGenericRepository<IEntity> where IEntity : BaseEntity
    {
        IQueryable<IEntity> GetAll();
        Task<IEntity> GetAsync(Guid id);
        Task CreateAsync(IEntity entity);
        Task UpdateAsync(Guid id,IEntity entity);
    }
}
