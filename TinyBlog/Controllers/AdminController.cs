using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    [RequiresRole(UserRole.Admin)]
    public class AdminController : Controller
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IContentService _contentService;

        public AdminController(ISiteSettingsService siteSettingsService, IContentService contentService)
        {
            _siteSettingsService = siteSettingsService;
            _contentService = contentService;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult SiteSettings()
        {
            Site siteSettings = _siteSettingsService.GetSiteSettings();
            return View(siteSettings);            
        }


        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Save(Site siteSettings)
        {
            _siteSettingsService.SaveSiteSettings(siteSettings);
            return View("Index");
        }

        public virtual ActionResult ModerateComments()
        {
            IList<Comment> comments = _contentService.GetUnapprovedComments();

            return View("Comments", comments);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ApproveComment(int id)
        {
            var status = _contentService.ApproveComment(id);

            if (status is Failure) Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            if (Request.IsAjaxRequest())
            {
                return new JsonResult();
            }
            return ModerateComments();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult DeleteComment(int id)
        {
            var status = _contentService.DeleteComment(id);

            if (status is Failure) Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (Request.IsAjaxRequest())
            {
                return new JsonResult();
            }
            return ModerateComments();
        }
    }
}
