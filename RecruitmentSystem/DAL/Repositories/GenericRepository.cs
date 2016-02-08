using RecruitmentSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RecruitmentSystem.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
          where TEntity : class
    {
        internal DbContext _context;
        internal readonly DbSet<TEntity> _dbset;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbset.AsEnumerable<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}