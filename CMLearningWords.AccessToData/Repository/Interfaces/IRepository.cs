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
        IQueryable<T> GetAllIQueryableWithInclude(Expression<Func<T, object>> predicate = null);
        //Get element form database by Id
        T Get(long id);
        //Add element to database
        void Add(T item);
        //Update element by entity
        void Update(T item);
        //Remove element form database by entity
        void Remove(T imem);
        //Save all actions
        void Save();
    }
}
