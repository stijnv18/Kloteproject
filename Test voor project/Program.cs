using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoteSaverApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the note content:");
            string content = Console.ReadLine();

            Console.WriteLine("Enter the file path:");
            string filePath = Console.ReadLine();

            var note = new
            {
                Content = content,
                FilePath = filePath
            };

            string apiUrl = "https://localhost:8888/saveNote"; // Adjust the URL if needed

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(note);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response from API: " + result);
                        string aa = Console.ReadLine();
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode} - {error}");
                        string aa = Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while sending the request: " + ex.Message);
                }
            }
        }
    }
}
