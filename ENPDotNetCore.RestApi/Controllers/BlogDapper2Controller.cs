using Dapper;
using ENPDotNetCore.RestApi.Models;
using ENPDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ENPDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapper2Controller : ControllerBase
{
    //private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    private readonly DapperService _dapperService;

    public BlogDapper2Controller(DapperService dapperService)
    {
        _dapperService = dapperService;
    }

    [HttpGet]
    public IActionResult GetBlogs()   
    {
        string query = "select * from Tbl_Blog";
        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
        var lst = _dapperService.Query<BlogModel>(query);
        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var item = FindById(id);//object default is null
        if (item is null)
        {  
            return NotFound("No data found");
        }
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blogModel)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //int result = db.Execute(query, blogModel);
        int result = _dapperService.Execute(query, blogModel);
        string message = result > 0 ? "Saving Successful" : "Saving Failed";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id,BlogModel blogModel)
    {
        var item = FindById(id);
        if(item is null)
        {
            return NotFound("No data Found");
        }
        blogModel.BlogId = id;
        string query = @"UPDATE [dbo].[Tbl_Blog]
                SET [BlogTitle] = @BlogTitle
                ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
                 WHERE BLogId =@BlogId";
        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //int result = db.Execute(query,blogModel);
        int result = _dapperService.Execute(query, blogModel);
        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blogModel)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data Found");
        }
        string conditions = string.Empty;

        if(!string.IsNullOrEmpty(blogModel.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(blogModel.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent, ";
        }
        if(conditions.Length == 0)
        {
            return NotFound("no data found");
        }

        conditions = conditions.Substring(0, conditions.Length - 2);

        blogModel.BlogId = id;

        string query = $@"UPDATE [dbo].[Tbl_Blog]
                SET {conditions} 
                WHERE BlogId = @BlogId";

        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //int result = db.Execute(query, blogModel);
        int result = _dapperService.Execute(query, blogModel);
        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = FindById(id);
        if (item is null)
        {
            return NotFound("No data Found");
        }
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

        int result = _dapperService.Execute(query, new BlogModel { BlogId = id });
        string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
        return Ok(message);
    }

    private BlogModel? FindById(int id)
    {
        string query = "select * from tbl_blog where blogid = @BlogId";
        //using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
        return item;
    }
}
