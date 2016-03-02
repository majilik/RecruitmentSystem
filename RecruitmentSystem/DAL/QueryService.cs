using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace RecruitmentSystem.DAL
{
    /// <summary>
    /// Represents a service that lies between the DbContext (Data Layer) and
    /// any Controller (Business Layer) that needs to query a data source.
    /// It is meant to provide the least amount of abstraction over Entity Framework
    /// possible, while still separating the actual query logic from the business layer
    /// and supporting easy Unit Tests with or without mocking.
    /// </summary>
    /// <typeparam name="T">Any class configured to be persisted via a DbContext.</typeparam>
    public class QueryService<T> where T : class
    {
        private IDbContextFactory<DbContext> _contextFactory;

        /// <summary>
        /// Default constructor. Injects the default implementation of IDbContextFactory,
        /// RecruitmentContextFactory, via constructor chaining.
        /// </summary>
        public QueryService() : this(new RecruitmentContextFactory())
        {
        }

        /// <summary>
        /// Constructs a new QueryService and injects an implementation of IDbContextFactory
        /// used to construct a new DbContext for each query.
        /// </summary>
        /// <param name="contextFactory"></param>
        public QueryService(IDbContextFactory<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationProperties">A lambda expression tree representing a query.</param>
        /// <returns>A collection of the result of the query, or an empty collection.</returns>
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (DbContext context = _contextFactory.Create())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include(navigationProperty);
                }

                list = dbQuery.AsNoTracking().ToList();
            }

            return list ?? new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties">A lambda expression tree representing a query.</param>
        /// <returns>A collection of the result of the query, or an empty collection.</returns>
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

            return list ?? new List<T>();
        }

        /// <summary>
        /// Usage: QueryService.getSingle(e => e.Id.Equals(someId));
        /// </summary>
        /// <param name="where">A lamda expression returning true or false depending on 
        /// the result of evaluating the expression.</param>
        /// <param name="navigationProperties">A lambda expression tree representing a query.</param>
        /// <returns>An entity representing the result of the query, or null if no persisted entity
        /// matches the expression.</returns>
        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item;
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

        /// <summary>
        /// Adds the <paramref name="items"/> to the DbContext returned by
        /// <see cref="IDbContextFactory{TContext}"/> injected in the constructor
        /// for this instance. The changes are then persisted to the underlying
        /// databse.
        /// </summary>
        /// <param name="items">A variable number of arguments of type <typeparamref name="T"/>,
        /// or an array.</param>
        public virtual void Add(params T[] entities)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T entity in entities)
                {
                    context.Set<T>().Attach(entity);
                    context.Entry(entity).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Modifies the <paramref name="items"/> in the DbContext returned by
        /// <see cref="IDbContextFactory{TContext}"/> injected in the constructor
        /// for this instance. The changes are then persisted to the underlying
        /// database.
        /// </summary>
        /// <param name="items">A variable number of arguments of type <typeparamref name="T"/>,
        /// or an array.</param>
        public virtual void Update(params T[] entities)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T entity in entities)
                {
                    context.Entry(entity).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the <paramref name="items"/> in the DbContext returned by
        /// <see cref="IDbContextFactory{TContext}"/> injected in the constructor
        /// for this instance. The changes are then persisted to the underlying
        /// database.
        /// </summary>
        /// <param name="items">A variable number of arguments of type <typeparamref name="T"/>,
        /// or an array.</param>
        public virtual void Remove(params T[] entities)
        {
            using (DbContext context = _contextFactory.Create())
            {
                foreach (T entity in entities)
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
    }
}