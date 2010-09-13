using System.Collections.Generic;
using System.Web.Mvc;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    public class PagesController : ContentControllerBase
    {
        public PagesController(IContentService contentService) : base(contentService){}

        public virtual ActionResult Index()
        {
            IList<Post> staticPosts = ContentService.GetPages();

            return View(staticPosts);
        }

    }
}
