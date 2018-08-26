using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMLearningWords.AccessToData.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        //Find elemets of T objects by expression (with include of not).
        IQueryable<T> FindWithInclude(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include = null);
        //Get All elemets of T objects (with include of not).
        IQueryable<T> GetAllIQueryableWithInclude(Expression<Func<T, object>> predicate = null, Expression<Func<T, object>> thanPredicate = null);
        //
        Task<IQueryable<T>> GetAllIQueryableWithIncludesAsync(Expression<Func<T, object>> predicate = null, Expression<Func<T, object>> thanPredicate = null);
        //Get element form database by Id
        Task<T> GetById(long id);
        //Add element to database
        Task Add(T item);
        //Add many elements to database
        Task AddMany(IEnumerable<T> items);
        //Update element by entity
        Task Update(T item);
        //Remove element form database by entity
        Task Remove(T imem);
        //Remove element by id
        Task RemoveById(long id);
    }
}
