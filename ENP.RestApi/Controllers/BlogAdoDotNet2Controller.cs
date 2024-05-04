using ENPDotNetCore.RestApi.Models;
using ENPDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace ENPDotNetCore.RestApi.Controllers
{
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
            string query = "select * from tbl_blog where BlogId = @BlogId";

            //if not use params, 
            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            //if use params, write below, params means we can add other property by using comma 
           
            var item = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            if(item is null)
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
                  new AdoDotNetParameter("@BlogTitle",model.BlogTitle),
                   new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
                    new AdoDotNetParameter("@BlogContent", model.BlogContent)
                );
           
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
       ;
            return Ok(message);
        }

    }
}
