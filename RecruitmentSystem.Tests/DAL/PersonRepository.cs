using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecruitmentSystem.DAL.Interfaces;
using RecruitmentSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentSystem.Tests.DAL
{
    [TestClass]
    public class PersonRepository
    {
        [TestMethod]
        public void Can_Return_All_Persons()
        {
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            IList<Person> persons = new List<Person>{ It.IsAny<Person>() };
            mock.Setup(mr => mr.GetAll()).Returns(persons);
            IPersonRepository rep = mock.Object;

            IEnumerable<Person> allPersons = rep.GetAll();

            Assert.IsNotNull(allPersons);
            Assert.AreEqual(1, allPersons.Count());

        }

        [TestMethod]
        public void Can_Insert_Any_Person()
        {
            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            IList<Person> persons = new List<Person>{};

            mock.Setup(mr => mr.GetAll()).Returns(persons);
            mock.Setup(r => r.Insert(It.IsAny<Person>())).Callback(() => persons.Add(It.IsAny<Person>()));
            IPersonRepository rep = mock.Object;
            Assert.AreEqual(0, rep.GetAll().Count());

            rep.Insert(It.IsAny<Person>());

            IEnumerable<Person> allPersons = rep.GetAll();

            Assert.AreEqual(1, allPersons.Count());
        }
    }
}