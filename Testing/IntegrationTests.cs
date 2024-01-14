using Classes;
using ServerApi.Controllers;
using ServerApi.Functions;

namespace Testing
{
    [TestClass]
    public class IntegrationTests
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


        [TestMethod, TestCategory("IntegrationTest")]
        [DataRow("novypouzivatel@gmail.com", "heslo")]
        public async Task SignUp(string email, string password)
        {
            testId = "Sign001I";
            parameters = email + ", " + password;
            testStartTime = DateTime.Now;
            SignInController signInController = new SignInController();
            UserController userController = new UserController();

            //asks for unique code
            assertionMessage = "Assert failed - could not sign up.";
            string? result = await signInController.SignUpAsync(email, password);
            Assert.IsTrue(string.IsNullOrEmpty(result), assertionMessage);
            log += "user signed up,";

            //gets code
            assertionMessage = "Assert failed - code not found";
            string? code = SignInFunctions.GetCode(email);
            Assert.IsNotNull(code, assertionMessage);
            log += "code obtained,";

            //checks code is valid
            assertionMessage = "Assert failed - codes do not match.";
            result = await signInController.VerifyAsync(code);
            Assert.IsTrue(!string.IsNullOrEmpty(result), assertionMessage);
            log += "code valid,";

            //deletes user
            assertionMessage = "Assert failed - could not delete user.";
            result = userController.DeleteUser(email);
            Assert.IsTrue(!string.IsNullOrEmpty(result));

            assertionMessage = string.Empty;
            log += "user deleted";

        }

        [TestMethod, TestCategory("IntegrationTest")]
        [DataRow("pouzivatel", "pouzivatelka")]
        public void Follow(string follower, string following)
        {
            testId = "User001I";
            parameters = follower + ", " + following;
            testStartTime = DateTime.Now;
            UserController userController = new UserController();

            //starts following
            assertionMessage = "Assert failed - could not start following.";
            bool result = userController.StartFollow(follower,following);
            Assert.IsTrue(result);
            log += "follow successful,";

            //stops following
            assertionMessage = "Assert failed - could not unfollow.";
            result = userController.StopFollow(follower,following);
            Assert.IsTrue(result);

            assertionMessage = string.Empty;
            log += "unfollow successful";
        }
    }
}