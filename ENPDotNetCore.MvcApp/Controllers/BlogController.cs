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
            List<BlogModel> lst = await _db.Blogs.ToListAsync();
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
    }
}
