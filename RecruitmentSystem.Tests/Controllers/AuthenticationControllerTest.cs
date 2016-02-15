using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecruitmentSystem;
using RecruitmentSystem.Controllers;
using Moq;
using RecruitmentSystem.Models;

namespace RecruitmentSystem.Tests.Controllers
{
    [TestClass]
    public class AuthenticationControllerTest
    {
        [TestMethod]
        public void GetRegister()
        {
            // Arrange
            AuthenticationController controller = new AuthenticationController();

            // Act
            ViewResult result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
