using System;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Classes;

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

        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(endpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var c = response.Content;
                        if (typeof(T) == typeof(string))
                        {
                            return (T)(object)response.Content.ReadAsStringAsync().Result;
                        }
                        return await response.Content.ReadAsAsync<T?>();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        return default(T?);
                    }
                }
            }
            catch
            {
                return default;
            }
        }
        public static async Task<T?> PostAsync<T>(string endpoint, HttpContent content)
        {
            using (HttpClient client = CreateHttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    return default;
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
