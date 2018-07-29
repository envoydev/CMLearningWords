using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CMLearningWords.AccessToData.Context;
using CMLearningWords.AccessToData.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMLearningWords.AccessToData.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //Contection
        protected readonly ApplicationContext Context;
        //created entity by generic
        private DbSet<T> entity;
        //variable for closed connections
        private bool disposed = false;

        //Create new connection
        public Repository(ApplicationContext context)
        {
            this.Context = context;
            entity = context.Set<T>();
        }

        //Find T objects (with Include or not) by expression (example: x => x.id == id)/(example: x => x.bool == bool, x => x.object).
        public virtual IQueryable<T> FindWithInclude(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> include = null)
        {
            if (predicate != null && include == null)
                return entity.Where(predicate);
            else if (predicate != null && include != null)
                return entity.Where(predicate).Include(include);
            return null;

        }

        //Find All objects of T (with include or not) expression(example: x => x.object)
        public virtual IQueryable<T> GetAllIQueryableWithInclude(Expression<Func<T, object>> include = null)
        {
            if (include != null)
                return entity.Include(include);
            return entity;
        }

        //Get one object of T by id
        public T Get(long id)
        {
            return entity.Find(id) ?? null;
        }

        //Add new object of T to database
        public virtual void Add(T item)
        {
            if (item != null)
                entity.Add(item);
        }

        //Update item of object T
        public virtual void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        //Remove object T form table of T
        public virtual void Remove(T item)
        {
            if (item != null)
                entity.Remove(item);
        }

        //Save all changes
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        //Close connections
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    Context.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
