using Microsoft.EntityFrameworkCore;

namespace ENPDotNetCore.RestApiWithNLayer.Features.Blog
{
    public class BL_BLog
    {
        private readonly DA_Blog _dA_Blog;
        
        public BL_BLog()
        {
            _dA_Blog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _dA_Blog.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id)
        {
            var item = _dA_Blog.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            var result = _dA_Blog.CreateBlog(requestModel);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            int result = _dA_Blog.UpdateBlog(id, requestModel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            int reult = _dA_Blog.PatchBlog(id, requestModel);
            return reult;
        }


        public int DeleteBlog(int id)
        { 
            int result =_dA_Blog.DeleteBlog(id);
            return result;
        }
    }
}
