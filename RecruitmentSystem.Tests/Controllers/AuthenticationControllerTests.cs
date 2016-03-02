using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecruitmentSystem.DAL.Authorization.Interfaces;
using RecruitmentSystem.Models.ViewModel;
using RecruitmentSystem.Security;
using System.Linq;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers.Tests
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        [TestMethod]
        public void LoginReturnsCorrectViewTest()
        {
            AuthenticationController controller = new AuthenticationController();

            ViewResult result = controller.Login() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginReturnsCorrectViewIfLoginSucceedsTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            LoginView loginView = new LoginView();
            loginView.Username = "user";

            umMock.Setup(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username)))).Returns(true);
            umMock.Setup(um => um.LoginCheck(It.Is<LoginView>(lw => lw.Equals(loginView)))).Returns(true);

            RedirectToRouteResult result = controller.Login(loginView) as RedirectToRouteResult;

            Assert.IsNotNull(result);

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);

            umMock.Verify(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username))), Times.Once());
            umMock.Verify(um => um.LoginCheck(It.Is<LoginView>(lw => lw.Equals(loginView))), Times.Once());
            formsAuthMock.Verify(fam => fam.SetAuthCookie(It.Is<string>(s => s.Equals(loginView.Username)), false), Times.Once());
        }

        [TestMethod]
        public void LoginReturnsCorrectViewIfPasswordWrongTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            LoginView loginView = new LoginView();
            loginView.Username = "user";

            umMock.Setup(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username)))).Returns(true);
            umMock.Setup(um => um.LoginCheck(It.Is<LoginView>(lw => lw.Equals(loginView)))).Returns(false);

            ViewResult result = controller.Login(loginView) as ViewResult;

            Assert.IsNotNull(result);
            ModelState paramState;
            Assert.IsTrue(controller.ModelState.TryGetValue("wrong_pass", out paramState));
            NUnit.Framework.Assert.DoesNotThrow(() =>
                paramState.Errors.Single(s => s.ErrorMessage.Equals("Wrong password.")));

            umMock.Verify(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username))), Times.Once());
            umMock.Verify(um => um.LoginCheck(It.Is<LoginView>(lw => lw.Equals(loginView))), Times.Once());
        }

        [TestMethod]
        public void LoginReturnsCorrectViewIfUsernameNotFoundTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            LoginView loginView = new LoginView();
            loginView.Username = "user";

            umMock.Setup(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username)))).Returns(false);

            ViewResult result = controller.Login(loginView) as ViewResult;

            Assert.IsNotNull(result);
            ModelState paramState;
            Assert.IsTrue(controller.ModelState.TryGetValue("non_existent_user", out paramState));
            NUnit.Framework.Assert.DoesNotThrow(() =>
                paramState.Errors.Single(s => s.ErrorMessage.Equals("User doesn't exist.")));

            umMock.Verify(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(loginView.Username))), Times.Once());
        }

        [TestMethod]
        public void LoginReturnsCorrectViewIfModelStateIsInvalidTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            controller.ModelState.AddModelError("key", "error message");

            ViewResult result = controller.Login(new LoginView()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SignOutSignsUserOutTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            controller.SignOut();

            formsAuthMock.Verify(fam => fam.SignOut(), Times.Once());
        }

        [TestMethod]
        public void SignOutRedirectsToHomeTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            Mock<IFormsAuthenticationWrap> formsAuthMock = new Mock<IFormsAuthenticationWrap>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, formsAuthMock.Object);

            RedirectToRouteResult result = controller.SignOut() as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void RegisterReturnsCorrectViewTest()
        {
            AuthenticationController controller = new AuthenticationController();

            ViewResult result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterReturnsCorrectViewOnTakenUsernameTest()
        {
            Mock<IUserManager> umMock = new Mock<IUserManager>();
            AuthenticationController controller = new AuthenticationController(umMock.Object, null);

            RegisterView registerView = new RegisterView();
            registerView.Username = "user";
            registerView.Password = registerView.PasswordVerify = "pass";

            umMock.Setup(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(registerView.Username)))).Returns(true);

            ViewResult result = controller.Register(registerView) as ViewResult;

            Assert.IsNotNull(result);
            ModelState paramState;
            Assert.IsTrue(controller.ModelState.TryGetValue("username_in_use", out paramState));
            NUnit.Framework.Assert.DoesNotThrow(() =>
                paramState.Errors.Single(s => s.ErrorMessage.Equals("Username already in use!")));

            umMock.Verify(um => um.IsUsernameInUse(It.Is<string>(s => s.Equals(registerView.Username))), Times.Once());
        }

        [TestMethod]
        public void RegisterReturnsCorrectViewOnNonMatchingPasswordsTest()
        {
            AuthenticationController controller = new AuthenticationController(null, null);

            RegisterView registerView = new RegisterView();
            registerView.Password = "123456";
            registerView.PasswordVerify = "abc123";
            
            ViewResult result = controller.Register(registerView) as ViewResult;
            Assert.IsNotNull(result);
            ModelState paramState;
            Assert.IsTrue(controller.ModelState.TryGetValue("verify_pass", out paramState));
            NUnit.Framework.Assert.DoesNotThrow(() =>
                paramState.Errors.Single(s => s.ErrorMessage.Equals("Password verification failed!")));
        }

        [TestMethod]
        public void RegisterTest1()
        {

        }
    }
}