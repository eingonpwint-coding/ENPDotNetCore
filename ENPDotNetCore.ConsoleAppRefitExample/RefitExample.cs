using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ENPDotNetCore.ConsoleAppRefitExample;

public  class RefitExample
{
    private readonly IBlogApi _service = RestService.For<IBlogApi>("https://localhost:7043"); // var cannot be used in the global variable
    public async Task RunAsync()
    {
        await ReadAsync();
        //await EditAsync(1);
        //await EditAsync(4);
        //await UpdateAsync(200, "test title", "test author", "test content");
        //await DeleteAsync(300);
        //await CreateAsync("adding title", "adding author", "adding content");

    }

    private async Task ReadAsync()
    {
        
        var lst = await _service.GetBlogs();
        foreach (var item in lst)
        {
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}");
            Console.WriteLine("---------------------------------");
        }
    }

    private async Task EditAsync(int id)
    {
        try
        {
            var item = await _service.GetBlog(id);
            Console.WriteLine($"Id => {item.BlogId}");
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}");
            Console.WriteLine("---------------------------------");
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());//show detail
        }   
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel model = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var message = await _service.CreateBlog(model);
        Console.WriteLine(message);
    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        //need to add try catch
        BlogModel model = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        try
        {
            var message = await _service.UpdateBlog(id, model);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());//show detail
        }
    }


    private async Task DeleteAsync(int id)
    {
        try
        {
            var message = await _service.DeleteBlog(id);
            Console.WriteLine(message);
        }
        catch (ApiException ex)
        {
            Console.WriteLine(ex.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());//show detail
        }
    }
}
