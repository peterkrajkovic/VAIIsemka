using System;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace ClientApp.Functions
{
    public class Request
    {
        private static readonly string BaseAddress = "https://localhost:7271";

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        public static async Task<string?> GetAsync(string endpoint)
        {
            using (HttpClient httpClient = CreateHttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }

        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            using (HttpClient httpClient = CreateHttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return default(T);
                }
            }
        }
        public static async Task<T?> PostAsync<T>(string endpoint, HttpContent content)
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return default(T);
                }
            }
        }

        public static async Task<bool> SignUpAsync(string address, string password)
        {
            string signUpEndpoint = $"/api/SignIn/SignUp/{address},{password}";
            return await GetAsync<bool>(signUpEndpoint);
        }

        public static async Task<string?> Login(string address, string password)
        {
            string loginEndpoint = $"/api/SignIn/SignIn/{address},{password}";
            return await GetAsync(loginEndpoint);
        }

        public static async Task<string?> Verify(string code)
        {
            string verifyEndpoint = $"/api/SignIn/Verify/{code}";
            return await GetAsync(verifyEndpoint);
        }

        public static async Task<bool> Logout(string guid)
        {
            string logoutEndpoint = $"/api/SignIn/LogOut/{guid}";
            return await GetAsync<bool>(logoutEndpoint);
        }

        public static async Task<bool> CheckGuid(string guid)
        {
            string checkGuidEndpoint = $"/api/SignIn/CheckGuid/{guid}";
            return await GetAsync<bool>(checkGuidEndpoint);
        }

        internal static async Task<bool> UpdateProfile(string guid, string name, string username, DateTime date, string country, byte[]? picture)
        {
            string updateProfileEndpoint = $"/api/SignIn/UpdateProfile";
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("guid", guid),
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("date", date.ToString("yyyy-MM-dd")), // Format the date as needed
                new KeyValuePair<string, string>("picture", Convert.ToBase64String(picture ?? Array.Empty<byte>()))
            });
            return await PostAsync<bool>(updateProfileEndpoint,content);
        }
    }
}
