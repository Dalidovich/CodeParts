using CodeParts.Data.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<bool> createAsync(T entity);
        public Task<bool> deleteAsync(T entity);
        public IQueryable<T> GetAll();
    }
}
