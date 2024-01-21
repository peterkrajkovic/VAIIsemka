using System.Diagnostics.Metrics;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Classes;

namespace ClientApp.Functions
{
    public class Calls
    {

        public static async Task<string?> SignUpAsync(string address, string password)
        {
            string signUpEndpoint = $"/api/SignIn/SignUp/{address},{password}";
            return await Request.GetAsync<string>(signUpEndpoint);
        }
        public static async Task<string?> Followers(string username)
        {
            string endpoint = $"/api/User/FollowersNumber/{username}";
            return await Request.GetAsync<string>(endpoint);
        }
        public static async Task<string?> Followings(string username)
        {
            string endpoint = $"/api/User/FollowingsNumber/{username}";
            return await Request.GetAsync<string>(endpoint);
        }

        public static async Task<string?> Login(string address, string password)
        {
            string loginEndpoint = $"/api/SignIn/SignIn/{address},{password}";
            return await Request.GetAsync<string>(loginEndpoint);
        }

        public static async Task<string?> Verify(string code)
        {
            string verifyEndpoint = $"/api/SignIn/Verify/{code}";
            return await Request.GetAsync<string>(verifyEndpoint);
        }


        public static async Task<bool> CheckGuid(string guid)
        {
            string checkGuidEndpoint = $"/api/SignIn/CheckGuid/{guid}";
            return await Request.GetAsync<bool>(checkGuidEndpoint);
        }
        public static async Task<List<Post>?> GetPosts(string username)
        {
            string getPostsEndpoint = $"/api/User/GetPosts/{username}";
            return await Request.GetAsync<List<Post>>(getPostsEndpoint);
        }
        public static async Task<List<Post>?> GetImages(string guid)
        {
            string getPostsEndpoint = $"/api/User/GetImages/{guid}";
            return await Request.GetAsync<List<Post>>(getPostsEndpoint);
        }

        public static async Task<bool> UpdateProfile(MultipartFormDataContent formData)
        {
            string updateProfileEndpoint = $"/api/User/UpdateProfile";
          
            return await Request.PostAsync<bool>(updateProfileEndpoint, formData);
        }

       
        public static async Task<bool> IsUsernameFree(string name, string guid)
        {
            string endpoint = $"/api/User/IsUsernameFree/{name},{guid}";
           
            return await Request.GetAsync<bool>(endpoint);
        }

        public static async Task<string?> PasswordRecovery(string email, string password)
        {
            string endpoint = $"/api/SignIn/PasswordRecovery/{email},{password}";
            return await Request.GetAsync<string>(endpoint);
        }
        public static async Task<string?> Resend(string email)
        {
            string resendEndpoint = $"/api/SignIn/Resend/{email}";
            return await Request.GetAsync<string>(resendEndpoint);
        }

        public static async Task<Classes.User?> GetUser(string guid)
        {
            string endpoint = $"/api/User/GetUser/{guid}";
            return await Request.GetAsync<Classes.User?>(endpoint);
        }
        public static async Task<bool> IsMe(string guid, string username)
        {
            string endpoint = $"/api/User/IsMe/{guid},{username}";
            return await Request.GetAsync<bool>(endpoint);
        }
        public static async Task<bool> IsFollowing(string guid, string username)
        {
            string endpoint = $"/api/User/IsFollowing/{guid},{username}";
            return await Request.GetAsync<bool>(endpoint);
        }

        public static async Task<Classes.User?> GetUser(string guid, string username)
        {
            string endpoint = $"/api/User/GetUser/{guid},{username}";
            return await Request.GetAsync<Classes.User?>(endpoint);
        }
    }
}
