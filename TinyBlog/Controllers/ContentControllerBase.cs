using System.Web.Mvc;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    
    public abstract class ContentControllerBase: Controller
    {
        public IContentService ContentService { get; set; }

        protected ContentControllerBase(IContentService contentService)
        {
            ContentService = contentService;
        }


    }
}
