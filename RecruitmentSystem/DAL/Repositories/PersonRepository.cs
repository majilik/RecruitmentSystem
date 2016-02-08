using RecruitmentSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using RecruitmentSystem.Models;
using System.Data.Entity;

namespace RecruitmentSystem.DAL.Repositories
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private RecruitmentContext _context;

        public PersonRepository(RecruitmentContext context)
        {
            _context = context;
        }

        public virtual void Delete(long id)
        {
            Delete(_context.Persons.Find(id));
        }

        public virtual void Delete(Person person)
        {
            if (_context.Entry(person).State == EntityState.Detached)
            {
                _context.Persons.Attach(person);
            }
            _context.Persons.Remove(person);
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons;
        }

        public Person GetByID(long id)
        {
            return _context.Persons.Find(id);
        }

        public void Insert(Person person)
        {
            _context.Persons.Add(person);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.Persons.Attach(person);
            _context.Entry(person).State = EntityState.Modified;
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