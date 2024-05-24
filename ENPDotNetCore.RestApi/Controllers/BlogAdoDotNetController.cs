using ENPDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace ENPDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNetController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "select * from Tbl_Blog;";
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        //Console.WriteLine("Connection open");
        
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);//new query (cmd)
        DataTable dt = new DataTable();//create table in sql
        adapter.Fill(dt);// when click execute, result comes into dt
        connection.Close();
        //Console.WriteLine("Connection close");

        //List<BlogModel> lst = new List<BlogModel>();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    BlogModel blog = new BlogModel();
        //    //var data = dr["BlogId"]; output is object =>need to change int (BlogId is int)
        //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);//default 0
        //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);//default - null
        //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
        //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
        //    lst.Add(blog);

        //    BlogModel blog1 = new BlogModel
        //    {
        //        BlogId = Convert.ToInt32(dr["BlogId"]),//default 0
        //        BlogTitle = Convert.ToString(dr["BlogTitle"]),//default - null
        //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
        //        BlogContent = Convert.ToString(dr["BlogContent"])
        //    };
            
        //    lst.Add(blog);

        //}
        //AsEnumerable declare for use linq / select equal to foreach
        List<BlogModel> lst =dt.AsEnumerable().Select(dr=> new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),//default 0
            BlogTitle = Convert.ToString(dr["BlogTitle"]),//default - null
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        }).ToList();

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        string query = "select * from tbl_blog where BlogId = @BlogId";

        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0)
        {
            return NotFound("No data found.");
        }

        DataRow dr = dt.Rows[0];
        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        };
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
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        //Console.WriteLine("Connection open");
       
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Saving Successful" : "Saving Failed";
        return Ok(message);
        //return StatusCode(500,message):
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id,BlogModel blog)
    {
        string getQuery = "SELECT COUNT(*) FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand(getQuery, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        var count = (int)cmd.ExecuteScalar();
        if (count == 0) return NotFound("No data found");

        string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                SET [BlogTitle] = @BlogTitle
                ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
                 WHERE BLogId =@BlogId";
        SqlCommand cmd2 = new SqlCommand(updateQuery, connection);
        cmd2.Parameters.AddWithValue("@BlogId", id);
        cmd2.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd2.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd2.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd2.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Updating Successful" : "Updating Failed";
        return Ok(message);
    }

    //[HttpPatch("{id}")]
    //public IActionResult PatchBlog2(int id, BlogModel blog)
    //{
    //    string getQuery = "SELECT COUNT(*) FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

    //    using (SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString))
    //    {
    //        connection.Open();
    //        SqlCommand cmd = new SqlCommand(getQuery, connection);
    //        cmd.Parameters.AddWithValue("@BlogId", id);

    //        var count = (int)cmd.ExecuteScalar();
    //        if (count == 0) return NotFound("No data found");

    //        List<SqlParameter> parameters = new List<SqlParameter>();

    //        string conditions = string.Empty;

    //        if (!string.IsNullOrEmpty(blog.BlogTitle))
    //        {
    //            conditions += "[BlogTitle] = @BlogTitle, ";
    //            parameters.Add(new SqlParameter("@BlogTitle", SqlDbType.NVarChar) { Value = blog.BlogTitle });
    //        }
    //        if (!string.IsNullOrEmpty(blog.BlogAuthor))
    //        {
    //            conditions += "[BlogAuthor] = @BlogAuthor, ";
    //            parameters.Add(new SqlParameter("@BlogAuthor", SqlDbType.NVarChar) { Value = blog.BlogAuthor });
    //        }
    //        if (!string.IsNullOrEmpty(blog.BlogContent))
    //        {
    //            conditions += "[BlogContent] = @BlogContent, ";
    //            parameters.Add(new SqlParameter("@BlogContent", SqlDbType.NVarChar) { Value = blog.BlogContent });
    //        }

    //        conditions = conditions.TrimEnd(',', ' '); // Remove trailing comma and space

    //        if (string.IsNullOrEmpty(conditions))
    //        {
    //            return NotFound("No data found");
    //        }

    //        string updateQuery = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

    //        SqlCommand cmd2 = new SqlCommand(updateQuery, connection);
    //        cmd2.Parameters.AddWithValue("@BlogId", id);
    //        cmd2.Parameters.AddRange(parameters.ToArray());

    //        int result = cmd2.ExecuteNonQuery();
    //        connection.Close();

    //        string message = result > 0 ? "Updating Successful" : "Updating Failed";

    //        return Ok(message);
    //    }
    //}

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel blog)
    {
        string getQuery = "SELECT COUNT(*) FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

        using (SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(getQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            var count = (int)cmd.ExecuteScalar();
            if (count == 0) return NotFound("No data found");

            string conditions = string.Empty;


            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
               conditions += "[BlogContent] = @BlogContent, ";
            }

           conditions = conditions.TrimEnd(',', ' '); // Remove trailing comma and space

            if (string.IsNullOrEmpty(conditions))
            {
                return NotFound("No data found");
            }

            string updateQuery = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            SqlCommand cmd2 = new SqlCommand(updateQuery, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);

            // Add parameters for BlogTitle, BlogAuthor, and BlogContent
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd2.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd2.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd2.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }

            int result = cmd2.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";
        SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection open");
       
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
        return Ok(message);
    }

}
