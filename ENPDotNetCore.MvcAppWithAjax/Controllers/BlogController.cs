using ENPDotNetCore.MvcAppWithAjax.Database;
using ENPDotNetCore.MvcAppWithAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.MvcAppWithAjax.Controllers
{
    public class BlogController : Controller
    {
        //Constructor injection
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        //logical path => http://localhost:3000/blog/index
        //physical path => http://localhost:3000/blog/blogindex

        //[ActionName("Index")] //Ei
        //public IActionResult BlogIndex([FromServices] AppDbContext db) //method injection
        //{
        //    return View("BlogIndex");//Ei Ngon Pwint
        //}

        //[FromServices]
        //public AppDbContext db { get; set; } //property injection

        [ActionName("Index")] //Ei
        public IActionResult BlogIndex()
        {
            //select * from tbl_blog
            //select * from tbl_blog with (nolock)


            List<BlogEntity> lst = _db.Blogs.AsNoTracking().ToList();
            return View("BlogIndex", lst);//Ei Ngon Pwint
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogEntity blog)
        {
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();
            string message = result > 0 ? "Saving successful" : "Saving Failed";
            return Json(new { Message = message, IsSuccess = result > 0 });
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return RedirectToAction("Index");

            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogEntity blog)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null) return Json(new { Message = "No data found", IsSuccess = false });

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            // because of AsNoTracking , need to use entityState.modified

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Updating successful" : "Updating Failed";

            return Json(new { Message = message, IsSuccess = result > 0 });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogEntity blog)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blog.BlogId);
            if (item is null) return Json(new { Message = "No data found", IsSuccess = false });

            // because of AsNoTracking , need to use entityState.modified

            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();
            string message = result > 0 ? "Deleting successful" : "Deleting Failed";

            return Json(new { Message = message, IsSuccess = result > 0 });
        }

    }  
}
