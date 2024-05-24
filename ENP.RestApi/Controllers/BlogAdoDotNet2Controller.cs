using ENPDotNetCore.RestApi.Models;
using ENPDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace ENPDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNet2Controller : ControllerBase
{
    private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "select * from Tbl_Blog;";
        var lst = _adoDotNetService.Query<BlogModel>(query);

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        //if not use params, 
        //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
        //parameters[0] = new AdoDotNetParameter("@BlogId", id);
        //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

        //if use params, write below, params means we can add other property by using comma 

        var item = BlogById(id);
        if (item is null)
        {
            return NotFound("No data found");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel model)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        int result = _adoDotNetService.Execute(query,
             new AdoDotNetParameter("@BlogId", model.BlogId),
             new AdoDotNetParameter("@BlogTitle", model.BlogTitle),
             new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
             new AdoDotNetParameter("@BlogContent", model.BlogContent)
            );
        string message = result > 0 ? "Saving Successful" : "Saving Failed";
        return Ok(message);
        //return StatusCode(500,message):
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var item = BlogById(id);
        if (item is null)
        {
            return NotFound("No data found");
        }

        string query = @"UPDATE [dbo].[Tbl_Blog]
                SET [BlogTitle] = @BlogTitle
                ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
                 WHERE BlogId =@BlogId";
        int result = _adoDotNetService.Execute(query,
            new AdoDotNetParameter("@BlogId",id),
            new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
            new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetParameter("@BlogContent", blog.BlogContent)
            );
        string message = result > 0 ? "Updating Successful" : "Updating Failed";

        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        var item = BlogById(id);
        if (item is null)
        {
            return NotFound("No data found");
        }
        List<AdoDotNetParameter> lst = new List<AdoDotNetParameter>();
        string conditions = string.Empty;
        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle, ";
            lst.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor, ";
            lst.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
        }
        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent, ";
            lst.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
        }

        conditions = conditions.TrimEnd(',', ' '); // Remove trailing comma and space

        if (string.IsNullOrEmpty(conditions))
        {
            return NotFound("No data found");
        }
        lst.Add(new AdoDotNetParameter("@BlogId",id));
        string updateQuery = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
        int result = _adoDotNetService.Execute(updateQuery, lst.ToArray());
        
        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = BlogById(id);
        if (item is null)
        {
            return NotFound("No data found");
        }
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

        int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));

        string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
        ;
        return Ok(message);
    }

    private BlogModel BlogById(int id)
    {
        string query = "select * from tbl_blog where BlogId = @BlogId";
        var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
        return item;
    }
}



    



