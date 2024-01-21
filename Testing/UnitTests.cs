using ServerApi.Controllers;
using ServerApi.Database;
using ServerApi.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class UnitTests
    {
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

        [TestCleanup]
        public void TestCleanup()
        {
            // Log test results
            string testName = TestContext.TestName;
            string testCategory = TestContext.FullyQualifiedTestClassName;
            string result = TestContext.CurrentTestOutcome.ToString();
            TimeSpan duration = DateTime.Now - testStartTime;
            int durationSeconds = (int)duration.TotalSeconds;

            TestFunctions.LogTest(testId, durationSeconds, testName, testCategory, result, log, assertionMessage, parameters);
        }

        [TestMethod, TestCategory("UnitTest")]
        [DataRow("jankomrkva@gmail.com", "Password1")]
        public void Login(string email, string password)
        {
            testId = "Sign001U";
            parameters = email + ", " + password;
            testStartTime = DateTime.Now;

            assertionMessage = "Assert failed - login was not successful.";
            string? result = SignInFunctions.Login(email, password);
            Assert.IsTrue(!string.IsNullOrEmpty(result), assertionMessage);

            log += "login successful";
            assertionMessage = string.Empty;
        }

        [TestMethod, TestCategory("UnitTest")]
        [DataRow("valid.email@example.com", true)]
        [DataRow("invalid-email@example", false)]
        public void IsEmail(string email, bool expectedResult)
        {
            testId = "Input001U";
            parameters = email + ", " + expectedResult.ToString();
            testStartTime = DateTime.Now;

            assertionMessage = "Assert failed - email was not checked correctly.";
            bool actualResult = InputHandler.IsEmail(ref email);
            Assert.AreEqual(expectedResult, actualResult, assertionMessage);

            log += "email check successful";
            assertionMessage = string.Empty;
        }

        [TestMethod, TestCategory("UnitTest")]
        [DataRow("", null)]
        [DataRow("Hello <script>alert('malicious code');</script>World", "Hello World")]
        [DataRow("<p>Valid HTML</p>", "<p>Valid HTML</p>")]
        public void SanitizeInputString(string input, string expectedOutput)
        {
            testId = "Input002U";
            parameters = input + ", " + expectedOutput;
            testStartTime = DateTime.Now;

            assertionMessage = "Assert failed - input was not sanitized correctly.";
            string? actualOutput = InputHandler.SanitizeInputString(ref input);
            Assert.AreEqual(expectedOutput?.Trim(), actualOutput?.Trim(), assertionMessage);

            log += "sanitizing successful";
            assertionMessage = string.Empty;
        }

        [TestMethod, TestCategory("UnitTest")]
        [DataRow("jankomrkva", false)]
        [DataRow("volnyusername", true)]
        public void IsUsernameFree(string username, bool expectedResult)
        {
            testId = "User001U";
            parameters = username + ", " + expectedResult.ToString();
            testStartTime = DateTime.Now;

            assertionMessage = "Assert failed - checking if username free was not successful.";
            bool actualResult = UserFunctions.IsUsernameFree(username,"ssa");
            Assert.AreEqual(actualResult, expectedResult);

            log += "checking if username free successful";
            assertionMessage = string.Empty;
        }


        [TestMethod, TestCategory("UnitTest")]
        [DataRow("jankomrkva")]
        public void NumberOfFollowing(string username)
        {
            testId = "User002U";
            parameters = username;
            testStartTime = DateTime.Now;

            assertionMessage = "Assert failed - numbers of followings dont match.";
            int number = UserFunctions.NumberOfFollowing(username);
            Assert.AreEqual(number, 0, assertionMessage);

            log += "numbers of followings do match";
            assertionMessage = string.Empty;
        }

    }
}
