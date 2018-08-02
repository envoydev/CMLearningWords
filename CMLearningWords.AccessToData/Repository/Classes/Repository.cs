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
        public virtual IQueryable<T> GetAllIQueryableWithInclude(Expression<Func<T, object>> include = null, Expression<Func<T, object>> thenInclude = null)
        {
            if (include != null && thenInclude != null)
                return entity.AsNoTracking().Include(include).Include(thenInclude);
            else if (include != null && thenInclude == null)
                return entity.AsNoTracking().Include(include).AsNoTracking();
            return entity.AsNoTracking();
        }

        public async virtual Task<IQueryable<T>> GetAllIQueryableWithIncludesAsync(Expression<Func<T, object>> include = null, Expression<Func<T, object>> thenInclude = null)
        {
            dynamic result;
            if (include != null && thenInclude != null)
                result = await entity.Include(include).Include(thenInclude).ToListAsync();
            else if (include != null && thenInclude == null)
                result = await entity.Include(include).ToListAsync();
            else
                result = await entity.ToListAsync();
            return result;
        }

        //Get one element by id
        public async virtual Task<T> GetById(long id)
        {
            return await entity.FindAsync(id) ?? null;
        }

        //Add new object of T to database
        public async virtual Task Add(T item)
        {
            if (item != null)
            {
                await entity.AddAsync(item);
                await Context.SaveChangesAsync();
            }
        }

        //Add new objecs of T to database
        public async virtual Task AddMany(IEnumerable<T> items)
        {
            if (items != null)
            {
                await entity.AddRangeAsync(items);
                await Context.SaveChangesAsync();
            }
        }

        //Update item of object T
        public async virtual Task Update(T item)
        {
            //await Context.Entry(item).State = EntityState.Modified;
            entity.Update(item);
            await Context.SaveChangesAsync();
        }

        //Remove object T form table of T
        public async virtual Task Remove(T item)
        {
            if (item != null)
            {
                entity.Remove(item);
                await Context.SaveChangesAsync();
            }
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
