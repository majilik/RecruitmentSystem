using RecruitmentSystem.Models;
using System;
using System.Collections.Generic;

namespace RecruitmentSystem.DAL.Interfaces
{
    public interface IPersonRepository : IDisposable
    {
        IEnumerable<Person> GetAll();
        Person GetByID(long id);
        void Insert(Person person);
        void Delete(long id);
        void Delete(Person person);
        void Update(Person person);
        void Save();
    }
}
