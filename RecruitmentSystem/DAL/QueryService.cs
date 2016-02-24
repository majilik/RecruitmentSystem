using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace RecruitmentSystem.DAL
{
    public class QueryService<T> where T : class
    {
        private IDbContextFactory<DbContext> _contextFactory;

        public QueryService() : this (new RecruitmentContextFactory())
        {
        }

        public QueryService(IDbContextFactory<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (DbContext context = _contextFactory.Create())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery.AsNoTracking().ToList();
            }

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (DbContext context = _contextFactory.Create())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }

                list = dbQuery.AsNoTracking().Where(where).ToList();
            }

            return list;
        }

        /// <summary>
        /// Usage: QueryService.getSingle(e => e.Id.Equals(someId));
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (DbContext context = _contextFactory.Create())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }

                item = dbQuery.AsNoTracking().FirstOrDefault(where);
            }

            return item;
        }

        public virtual void Add(params T[] items)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Update(params T[] items)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
    }
}