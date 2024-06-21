using ENPDotNetCore.MvcApp.Db;
using ENPDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDBContext _db;

        public BlogController(AppDBContext db)
        {
            _db = db;
        }

        [ActionName("Index")]
        public async Task<IActionResult> BlogIndex()
        {
            List<BlogModel> lst = await _db.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync();
            return View("BlogIndex",lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        { 
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogCreate(BlogModel blog)
        {
           await _db.Blogs.AddAsync(blog);
           var result = await _db.SaveChangesAsync();
           return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }
            return View("BlogEdit",item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(BlogModel blog,int id)
        {

            var item = await _db.Blogs
                .AsNoTracking().
                FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return Redirect("/Blog");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var item = await _db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }

            _db.Blogs.Remove(item);
            await _db.SaveChangesAsync();

            return Redirect("/Blog");
        }
    }
}
