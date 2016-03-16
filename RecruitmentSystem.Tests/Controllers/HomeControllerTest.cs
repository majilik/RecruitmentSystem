using NUnit.Framework;
using RecruitmentSystem.Controllers;
using System.Web.Mvc;

namespace RecruitmentSystem.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void IndexReturnsCorrectViewTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void UnauthorizedReturnsCorrectViewTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Unauthorized() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
