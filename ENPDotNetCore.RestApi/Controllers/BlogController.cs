using Dapper;
using ENPDotNetCore.RestApi.Db;
using ENPDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ENPDotNetCore.RestApi.Controllers;

//https:// localhost:3000 => domain url
//api/Blog ---end point
[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly AppDBContext _dbContext;
    public BlogController()
    {
        _dbContext = new AppDBContext();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var lst = _dbContext.Blogs.ToList();
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return NotFound("No data found");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blog)
    {
        _dbContext.Blogs.Add(blog);
        var result = _dbContext.SaveChanges();
        string message = result > 0 ? "Saving Successful" : "Saving Failed";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blog)
    {
        var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return NotFound("No data found");
        }
        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;
        var result = _dbContext.SaveChanges();
        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel blog)
    {
        var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return NotFound("No data found");
        }

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            item.BlogTitle = blog.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            item.BlogAuthor = blog.BlogAuthor;
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            item.BlogContent = blog.BlogContent;
        }

        var result = _dbContext.SaveChanges();
        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item == null)
        {
            return NotFound("No data found");
        }
        _dbContext.Blogs.Remove(item);
        int result = _dbContext.SaveChanges();

        string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
        return Ok(message);
    }
}
