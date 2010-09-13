using System.Web.Mvc;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    [ActionOutputCache(600)]
    public class HeaderController : Controller
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public HeaderController(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public virtual ActionResult Header()
        {
            Site settings = _siteSettingsService.GetSiteSettings();
            return View(settings);
        }

    }
}
