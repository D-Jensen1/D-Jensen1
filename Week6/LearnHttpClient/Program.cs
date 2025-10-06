using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LearnHttpClient;
public class Program
{
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        Timeout = TimeSpan.FromSeconds(30),
        MaxResponseContentBufferSize = 4 * 1024 * 1024
    };

    static async Task Main(string[] args)
    {
        //await GetHTMLFromRootAsync();//40X - non retryable //50x - retryable error
        await GetJsonAsync(sharedClient);
    }
    static async Task GetJsonAsync(HttpClient httpClient)
    {
        using HttpResponseMessage response = await httpClient.GetAsync("todos/3");

        response.EnsureSuccessStatusCode();

        // var jsonResponse = await response.Content.ReadAsStringAsync();
        // Console.WriteLine($"{jsonResponse}\n");
        Todo? todo3 = await response.Content.ReadFromJsonAsync<Todo>();
        Console.WriteLine($"UserID :{todo3?.userId}");
        Console.WriteLine($"TodoID :{todo3?.id}");
        Console.WriteLine($"Todo Title :{todo3?.title}");
        Console.WriteLine($"Completed? :{todo3?.completed}");
        // Expected output:
        //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
        //   {
        //     "userId": 1,
        //     "id": 3,
        //     "title": "fugiat veniam minus",
        //     "completed": false
        //   }
    }
    private static async Task GetHTMLFromRootAsync()
    {
        Task<HttpResponseMessage> getTask = sharedClient.GetAsync("/");
        while (!getTask.IsCompleted)
        {
            Console.Write(".");
            await Task.Delay(100);
        }
        Console.WriteLine("done");
        HttpResponseMessage response = getTask.Result;

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"http://http.cat/{(int)response.StatusCode}");

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("******* headers ********");
            foreach (var item in response.Headers)
            {
                Console.WriteLine($"{item.Key}:{String.Join(',', item.Value)}");
            }

            Console.WriteLine("******* content headers********");
            foreach (var item in response.Content.Headers)
            {
                Console.WriteLine($"{item.Key}:{String.Join(',', item.Value)}");
            }

            Task<string> stringTask = response.Content.ReadAsStringAsync();
            while (!stringTask.IsCompleted)
            {
                Console.Write(".");
                await Task.Delay(100);
            }
            Console.WriteLine("done");
            Console.WriteLine(stringTask.Result);
        }
    }

    //public record class Todo {
    //    public int UserID { get; set; }
    //    public int Id { get; set; }
    //    public string? Title { get; set; }
    //    public bool Completed { get; set; }
    //}

    public class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }

}