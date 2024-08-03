using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MauiApp2;

namespace MyMauiApp.Services
{
    public class ApiService
    {
        // Variables ----------------------------------------------------------------------------------------------------------------------------
        private readonly HttpClient _httpClient;

        // Constructor ----------------------------------------------------------------------------------------------------------------------------
        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        // Methodes ---------------------------------------------------------------------------------------------------------------------------
        public async Task<bool> PostDataAsync<T>(string url, T data)
        {
            try
            {
                // Serialize the data to JSON and post it
                var jsonData = JsonSerializer.Serialize(data);
                var response = await _httpClient.PostAsJsonAsync(url, data);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // Handle non-success status codes here
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, {responseBody}");

                return false;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network errors)
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }
        public async Task<List<DataBlog>> GetDataAsync(string url)
        {
            try
            {
                // Asking for connection to the url
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Getting the data in JSON format
                    string jsonData = await response.Content.ReadAsStringAsync();

                    // Parse the JSON data into a list of DataBlog objects
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var blogs = JsonSerializer.Deserialize<List<DataBlog>>(jsonData, options);

                    // 
                    return blogs ?? new List<DataBlog>();
                }
                else
                {
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while fetching data: {ex.Message}");
                throw;
            }
        }
    }
}
