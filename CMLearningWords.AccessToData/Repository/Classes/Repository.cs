using System;
using System.Collections.Generic;
using System.Linq;
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
