using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_BLog _bL_BLog;

        public BlogController()
        {
            _bL_BLog = new BL_BLog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _bL_BLog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _bL_BLog.GetBlog(id);
            if (item == null)
            {

                return NotFound("No data found");
            }



            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel requestModel)
        {
           
            var result = _bL_BLog.CreateBlog(requestModel);
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel requestModel)
        {
            var item = _bL_BLog.GetBlog(id);
            if (item == null)
            {

                return NotFound("No data found");
            }

            var result = _bL_BLog.UpdateBlog(id, requestModel);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchModelBlog(int id, BlogModel requestModel)
        {
            var item = _bL_BLog.GetBlog(id);
            if (item == null)
            {

                return NotFound("No data found");
            }

            var result = _bL_BLog.PatchBlog(id,requestModel);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _bL_BLog.GetBlog(id);
            if (item == null)
            {

                return NotFound("No data found");
            }
           
            int result = _bL_BLog.DeleteBlog(id);

            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            return Ok(message);
        }

    }
}
