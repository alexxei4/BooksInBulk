using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {



        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository (ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet
            
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            IQueryable<T> query = dbSet;
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            IQueryable<T> query = dbSet;
            dbSet.RemoveRange(items);
        }
    }
}
