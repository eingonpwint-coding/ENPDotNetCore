using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ENPDotNetCore.ConsoleAppRestClientExamples;

internal class RestClientExample
{
    private readonly RestClient _client = new RestClient(new Uri("https://localhost:7144"));
    
    private readonly string _blogEndpoint = "api/blog";

    public async Task RunAsync()
    {
        //await ReadAsync();
        //await EditAsync(3);
        //await EditAsync(56);
        //await CreateAsync("title9", "author99", "content999");
        //await UpdateAsync(21, "testing555", "testing5555", "testing555");
         //await EditAsync(21);
        //await PatchAsync(21, title:"testing patch");
        //await PatchAsync(21, content: "Updated Content");
        //await PatchAsync(21, author: "Updated author");
        
    
    }

    private async Task ReadAsync()
    {
        //RestRequest restRequest = new RestRequest(_blogEndpoint,Method.Get);
        //var response = await _client.GetAsync(restRequest);
        RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Get);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
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
        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine(JsonConvert.SerializeObject(item));
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}\n");
        }
        else
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }

    private async Task DeleteAsync(int id)
    {
        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
            //other process
            //continue
        }
        else
        {
            string message = response.Content!;
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
        RestRequest restRequest = new RestRequest(_blogEndpoint, Method.Post);
        restRequest.AddJsonBody(blog);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
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
        
        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
        restRequest.AddJsonBody(blog);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
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

        RestRequest restRequest = new RestRequest($"{_blogEndpoint}/{id}", Method.Patch);
        restRequest.AddJsonBody(blog);
        var response = await _client.ExecuteAsync(restRequest);
        if (response.IsSuccessStatusCode)
        {
            string message = response.Content!;
            Console.WriteLine(message);
        }
    }
}
