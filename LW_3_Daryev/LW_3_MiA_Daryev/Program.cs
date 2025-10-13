// See https://aka.ms/new-console-template for more information
// https://v2.jokeapi.dev/
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http;
using System.Text.Json;

namespace LW3Daryev
{
    struct Flags
    {
        public bool nsfw { get; set; }
        public bool religious { get; set; }
        public bool political { get; set; }
        public bool racist { get; set; }
        public bool sexist { get; set; }
        public bool explicit_ { get; set; }
    }
    internal class Joker
    {
        public bool error { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string setup { get; set; }
        public string delivery { get; set; }
        public Flags flags { get; set; }
        public int id { get; set; }
        public bool safe { get; set; }
        public string lang { get; set; }
    }


    class MainProgram
    {
        private static readonly HttpClient _http = new HttpClient
        {
            BaseAddress = new Uri("https://v2.jokeapi.dev/"),
            Timeout = TimeSpan.FromSeconds(15)
        };


        static async Task Main(string[] args)
        {
            Console.WriteLine("Request to  https://v2.jokeapi.dev/");
            try
            {
                var response = await _http.GetAsync("/joke/Programming");
                response.EnsureSuccessStatusCode(); 

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Raw JSON:");
                Console.WriteLine(json);

                var post = JsonSerializer.Deserialize<Joker>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });

                if (post != null)
                {
                    Console.WriteLine("\n=== Parsing result ===");
                    Console.WriteLine($"ID: {post.id}");
                    Console.WriteLine($"Category: {post.category}");
                    Console.WriteLine($"Type: {post.type}");
                    Console.WriteLine($"Setup: {post.setup}");
                    Console.WriteLine($"Delivery: {post.delivery}");
                    Console.WriteLine($"Lang: {post.lang}");
                    Console.WriteLine($"Flags: nsfw={post.flags.nsfw}, religious={post.flags.religious}, political={post.flags.political}, racist={post.flags.racist}, sexist={post.flags.sexist}, explicit_={post.flags.explicit_}");
                    Console.WriteLine($"Error: {post.error}");
                    Console.WriteLine($"Safe: {post.safe}");
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"JSON parsing error: {e.Message}");

            }
        }
    }
}





