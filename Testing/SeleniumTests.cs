using Classes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ServerApi.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class SeleniumTests
    {
        private IWebDriver driver;
        private TestContext testContextInstance;
        private DateTime testStartTime;
        private string assertionMessage;
        private string testId;
        private string log = string.Empty;
        private string parameters = string.Empty;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
        }

        [TestMethod, TestCategory("SeleniumTest")]
        public void Login()
        {
            testId = "Sign002S";
            testStartTime = DateTime.Now;
            assertionMessage = "Assert failed - could not login.";
            parameters = "jankomrkva@gmail.com, Password1";
            LoginFunction(driver);
            //check if redirected
            Assert.IsTrue(driver.Title.Contains("Home"), assertionMessage);
            assertionMessage = string.Empty;
            log += "login successful";
        }

        [TestMethod, TestCategory("SeleniumTest")]
        public void CheckUserStats()
        {
            testId = "User005S";
            parameters = "jankomrkva@gmail.com, Password1";
            testStartTime = DateTime.Now;
            assertionMessage = "Assert failed - could not login.";

            LoginFunction(driver);
            //check if redirected
            Assert.IsTrue(driver.Title.Contains("Home"));

            log += "login successful";
            IWebElement link = driver.FindElement(By.LinkText("Profile"));
            link.Click();
            assertionMessage = "Assert failed - redirect not successful.";
            //check if redirected
            Assert.IsTrue(driver.Title.Contains("User"));

            log += ", redirect successful";
            assertionMessage = "Assert failed - Usernames dont match.";
            IWebElement username = driver.FindElement(By.Id("username"));
            Assert.AreEqual(username.Text, "jankomrkva");

            assertionMessage = string.Empty;
            log += ", usernames match";
        }


        internal static void LoginFunction(IWebDriver driver)
        {
            
            driver.Navigate().GoToUrl("https://localhost:44310/signIn");

            //insert valid email and password
            IWebElement email = driver.FindElement(By.Name("LoginAddress"));
            email.SendKeys("jankomrkva@gmail.com");
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("Password1");

            // Submit the form
            IWebElement submitElement = driver.FindElement(By.Id("loginButton"));
            submitElement.Submit();
        }

        [TestMethod, TestCategory("SeleniumTest")]
        public void CheckResponsivity()
        {
            testId = "Resp001S";
            testStartTime = DateTime.Now;

            driver.Navigate().GoToUrl("https://localhost:44310/Index");
            driver.Manage().Window.Size = new System.Drawing.Size(375, 667);
            IWebElement navbarToggler = driver.FindElement(By.CssSelector(".navbar-toggler"));
            bool isNavbarTogglerVisible = navbarToggler.Displayed;

            // Click on the navbar toggler button
            if (isNavbarTogglerVisible)
            {
                navbarToggler.Click();
                log += "navbar collapsed";

                // Check if the link is visible after clicking the toggler button
                IWebElement link = driver.FindElement(By.CssSelector(".nav-link.text-white"));
                bool isLinkVisible = link.Displayed;
                assertionMessage = "Assert failed - toggler button not visible.";
                Assert.IsTrue(isLinkVisible, "The link is not visible after clicking the toggler button.");

                assertionMessage = string.Empty;
                log += ", navbar can expand";
            }
            else
            {
                assertionMessage = "Assert failed - Navbar toggler button is not visible.";
                Assert.Fail(assertionMessage);
            }

        }


        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
            // Log test results
            string testName = TestContext.TestName;
            string testCategory = TestContext.FullyQualifiedTestClassName;
            string result = TestContext.CurrentTestOutcome.ToString();
            TimeSpan duration = DateTime.Now - testStartTime;
            int durationSeconds = (int)duration.TotalSeconds;

            TestFunctions.LogTest(testId, durationSeconds, testName, testCategory, result, log, assertionMessage, parameters);
        }

       
    }
}
