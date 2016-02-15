using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecruitmentSystem;
using RecruitmentSystem.Security;
using Moq;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Tests.Controllers
{
    [TestClass]
    public class SecurityManagerTest
    {
        [TestMethod]
        public void PasswordHash()
        {
            string test_one = "111";
            string test_two = "two2";
            string test_three = "three";

            string result_one_a = SecurityManager.PasswordHash(test_one);
            string result_one_b = SecurityManager.PasswordHash(test_one);
            string result_two_a = SecurityManager.PasswordHash(test_two);
            string result_two_b = SecurityManager.PasswordHash(test_two);
            string result_three_a = SecurityManager.PasswordHash(test_three);
            string result_three_b = SecurityManager.PasswordHash(test_three);

            Assert.AreNotEqual(result_one_a, test_one);
            Assert.AreEqual(result_one_a, result_one_b);

            Assert.AreNotEqual(result_two_a, test_two);
            Assert.AreEqual(result_two_a, result_two_b);

            Assert.AreNotEqual(result_three_a, test_three);
            Assert.AreEqual(result_three_a, result_three_b);

            Assert.AreNotEqual(result_one_a, result_two_a);
            Assert.AreNotEqual(result_two_a, result_three_a);
            Assert.AreNotEqual(result_one_a, result_three_a);
        }
    }
}
