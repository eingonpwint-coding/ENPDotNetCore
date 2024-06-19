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

        public async Task<IActionResult> Index()
        {
            List<BlogModel> lst = await _db.Blogs.ToListAsync();
            return View(lst);
        }
    }
}
