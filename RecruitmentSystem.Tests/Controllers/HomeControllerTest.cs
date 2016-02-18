using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecruitmentSystem.Controllers;
using System.Web.Mvc;

namespace RecruitmentSystem.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexReturnsCorrectViewTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UnauthorizedReturnsCorrectViewTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Unauthorized() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
