using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyMauiApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> PostDataAsync<T>(string url, T data)
        {
            try
            {
                // Serialize the data to JSON and post it
                var response = await _httpClient.PostAsJsonAsync(url, data);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // Optionally, handle non-success status codes here
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
    }
}
