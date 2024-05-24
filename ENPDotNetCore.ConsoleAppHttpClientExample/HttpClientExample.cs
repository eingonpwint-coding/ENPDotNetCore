using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ENPDotNetCore.ConsoleAppHttpClientExample;

internal class HttpClientExample
{
    private readonly HttpClient _client = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:7144")
    };
    
    private readonly string _blogEndpoint = "api/blog";
    public async Task RunAsync()
    {
        //await ReadAsync();
        //await EditAsync(3);
        //await EditAsync(56);
        // await CreateAsync("title", "author", "content");
        //await UpdateAsync(20, "testing", "testing", "testing");
        // await EditAsync(20);
        //await PatchAsync(20, "testing patch");
        //await PatchAsync(20, content: "Updated Content");
        await PatchAsync(20, author: "Updated author");
        await EditAsync(20);
    }

    private async Task ReadAsync()
    {
        var response = await _client.GetAsync(_blogEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
            foreach (var item in lst)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}\n");
            }
        }

    }
    private async Task EditAsync(int id)
    {
        var response = await _client.GetAsync($"{_blogEndpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine(JsonConvert.SerializeObject(item));
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}\n");
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }

    private async Task DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            //other process
            //continue
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string blogJson = JsonConvert.SerializeObject(blog);
        HttpContent httpContent = new StringContent(blogJson,Encoding.UTF8,Application.Json);//accept myanmar language (adding encoding.UTF8)
        var response = await _client.PostAsync(_blogEndpoint, httpContent);
        if(response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }

    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string blogJson = JsonConvert.SerializeObject(blog);
        HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);//accept myanmar language (adding encoding.UTF8)
        var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }

    private async Task PatchAsync(int id, string? title = null, string? author = null, string? content = null)
    {
        BlogModel blog = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string blogJson = JsonConvert.SerializeObject(blog);
        HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);//accept myanmar language (adding encoding.UTF8)
        var response = await _client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
}
