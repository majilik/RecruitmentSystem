using RecruitmentSystem.Models;
using System;

namespace RecruitmentSystem.DAL
{
    public class UnitOfWork : IDisposable
    {
        private RecruitmentContext _context = new RecruitmentContext();
        private GenericRepository<Person> _personRepository;

        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new GenericRepository<Person>(_context);
                }
                return _personRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}